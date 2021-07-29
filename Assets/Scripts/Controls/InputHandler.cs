using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/InputHandler")]
public class InputHandler : ScriptableObject
{
    Camera defaultCamera;
    Camera activeCamera;
    Controls controls;
    Vector2 pan;
    float zoom;
    [SerializeField] private GameObject tempCharacter;


    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] MovementProcessor movementProcessor;

    public Camera ActiveCamera { get => activeCamera;}
    public Vector2 Pan { get => pan;}
    public float Zoom { get => zoom;}

    public void ManualAwake()
    {
        controls = new Controls();
        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        activeCamera = defaultCamera;

        controls.UniversalControls.Select.performed += _ => OnSelect();
        controls.UniversalControls.Deselect.performed += _ => OnDeselect();
        controls.UniversalControls.DoubleMovement.performed += _ => OnDoubleMovement();
        controls.UniversalControls.Pan.performed += _ => SetPan();
        controls.UniversalControls.Zoom.performed += _ => SetZoom();
        controls.UniversalControls.Interact.performed += _ => OnInteract();
        controls.UniversalControls.Inventory.performed += _ => OnInventoryToggle();
    }


    public void SetActiveCamera(Camera active)
    {
        activeCamera = active;
    }
    
    public Controls GetControls()
    {
        return controls;
    }

    public RaycastData GetRaycastHit()
    {
        Vector2 mousePosition = Vector2.zero;
        mousePosition = controls.UniversalControls.MousePosition.ReadValue<Vector2>();
        Ray ray = activeCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        return new RaycastData(Physics.Raycast(ray, out hit, Mathf.Infinity), hit);
    }

    public bool VerifyTag(RaycastData data, string tag)
    {
        if (data.Hit.collider.tag == tag && !EventSystem.current.IsPointerOverGameObject() && data.Hit.transform != null)
        {
            return true;
        }
            
        return false;
    }
    void DisplayError()
    {
        Debug.Log("error occurred");
    }

    public void SetZoom()
    {
        zoom = controls.UniversalControls.Zoom.ReadValue<float>();
    }

    public void SetPan()
    {
        pan = controls.UniversalControls.Pan.ReadValue<Vector2>();
    }
    void OnSelect()
    {
        RaycastData data = GetRaycastHit();
        switch (gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
                CombatManager tempCombatRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                if (tempCombatRef.CanContinue)
                {
                    if (tempCombatRef.GetTargeting() == true)
                    {
                        SendTarget(data, tempCombatRef);
                    }
                    else
                    {
                        Vector3 pos = GetLocation(data);
                        if(pos != Vector3.zero)
                        {
                            tempCombatRef.CombatMovement(GetLocation(data));
                        }                        
                    }
                }
                break;
            case GameStateEnum.Roaming:
                RoamingManager tempRoamingRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
                
                if (VerifyTag(data, "Terrain"))
                {
                    Vector3 pos = GetLocation(data);
                    if (pos != Vector3.zero)
                    {
                        tempRoamingRef.MoveToLocation(pos);
                    }
                }
                else if (VerifyTag(data, "Interactable"))
                {
                    tempRoamingRef.MoveAndInteract(data.Hit.point, data.Hit.transform.gameObject);
                }
                break;
            default:
                DisplayError();
                break;
        }
    }

    public void OnDeselect()
    {
        switch (gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.CombatAbilityDeselect();
                break;

            default:
                DisplayError();
                break;
        }
    }
    public void OnDoubleMovement()
    {
        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.CombatDoubleMove();
                break;

            default:
                DisplayError();
                break;
        }
    } 

    Vector3 GetLocation(RaycastData ray)
    {
        if (ray.HitBool && VerifyTag(ray, "Terrain"))
        {
            return ray.Hit.point;
        }
        return Vector3.zero;
    }

    void SendTarget(RaycastData ray, CombatManager tempRef)
    {
        if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Melee ||
            tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Ranged)
        {
            if (ray.HitBool && VerifyTag(ray, "Character") && ray.Hit.transform.GetComponent<Character>() != null &&
                movementProcessor.WithinRange(tempRef, ray.Hit.transform.GetComponent<Character>()))
            {
                if(ray.Hit.transform.GetComponent<Character>().IsPlayer() ^ tempRef.Character.IsPlayer())
                {
                    tempRef.CombatTarget(ray.Hit.transform.GetComponent<Character>());
                }
                else
                {
                    Debug.Log("Non recomended choice verify target");//TODO implement this UI check
                    tempRef.CombatTarget(ray.Hit.transform.GetComponent<Character>());
                }
                return;
            }

        }
        else if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Heal)
        {
            if (ray.HitBool && VerifyTag(ray, "Character") && ray.Hit.transform.GetComponent<Character>() != null &&
                movementProcessor.WithinRange(tempRef, ray.Hit.transform.GetComponent<Character>()))
            {
                if (!(ray.Hit.transform.GetComponent<Character>().IsPlayer() ^ tempRef.Character.IsPlayer()))
                {
                    tempRef.CombatTarget(ray.Hit.transform.GetComponent<Character>());
                }
                else
                {
                    Debug.Log("Non recomended choice verify target");//TODO implement this UI check
                    tempRef.CombatTarget(ray.Hit.transform.GetComponent<Character>());
                }
                return;
            }
        }
        else if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Splash)
        {
            //create method to create character at set position
            if (ray.HitBool && VerifyTag(ray, "Character") && ray.Hit.transform.GetComponent<Character>() != null &&
                movementProcessor.WithinRange(tempRef, ray.Hit.transform.GetComponent<Character>()))
            {
                if (!(ray.Hit.transform.GetComponent<Character>().IsPlayer() ^ tempRef.Character.IsPlayer()))
                {
                    tempRef.CombatTarget(ray.Hit.transform.GetComponent<Character>());
                }
                else
                {
                    Debug.Log("Non recomended choice verify target");//TODO implement this UI check
                    tempRef.CombatTarget(ray.Hit.transform.GetComponent<Character>());
                }
                return;
            }
            else if(ray.HitBool && VerifyTag(ray, "Terrain"))
            {
                GameObject temp2 = Instantiate(tempCharacter, ray.Hit.point, Quaternion.identity);
                Character temp1 = temp2.GetComponent<Character>();
                tempRef.CombatTarget(temp1);
                return;
            }
        }
        //returns in every other case where it worked
        //Debug.Log("something went wrong or user selected incorrectly");
        //display to user that they are selecting incorrectly   
    }  

    void OnInteract()
    {
        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Roaming:
                RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.Interact();
                break;

            default:
                DisplayError();
                break;
        }
    }
    void OnInventoryToggle()
    {
        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Roaming:
                RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.OpenInventory();
                break;

            default:
                DisplayError();
                break; ;
        }
    }
}
