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

        //combat
        controls.Combat.Select.performed += _ => CombatOnSelect();
        controls.Combat.Deselect.performed += _ => CombatOnDeselect();
        controls.Combat.DoubleMovement.performed += _ => CombatOnDoubleMovement();


        //roaming
        controls.Roaming.Interact.performed += _ => RoamingInteract();
        controls.Roaming.Inventory.performed += _ => RoamingInventory();
        controls.Roaming.Select.performed += _ => RoamingOnSelect();
        //controls.Roaming.Movement.performed += _ => RoamingMovement(controls.Roaming.Movement);

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
        Vector2 mousePosition = controls.Combat.MousePosition.ReadValue<Vector2>();
        Ray ray = activeCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        return new RaycastData(Physics.Raycast(ray, out hit, Mathf.Infinity), hit);
    }

    public bool VerifyTag(RaycastData data, string tag)
    {
        if (data.Hit.collider.tag == tag && !EventSystem.current.IsPointerOverGameObject() && data.Hit.transform != null)
            return true;
        return false;
    }
    void DisplayError()
    {
        Debug.Log("error occurred");
    }
    #region CombatMethods

    public void SetZoom()
    {
        zoom = controls.Combat.Zoom.ReadValue<float>();
    }

    public void SetPan()
    {
        pan = controls.Combat.Pan.ReadValue<Vector2>();
    }
    void CombatOnSelect()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            if (tempRef.CanContinue)
            {
                if (tempRef.GetTargeting() == true)
                {
                    CombatSendTarget(GetRaycastHit(), tempRef);
                }
                else
                {
                    CombatSendLocation(GetRaycastHit(), tempRef);
                }
            }
        }
        else
        {
            DisplayError();
        }
    } 
    public void CombatOnDeselect()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.CombatAbilityDeselect();
        }
        else
        {
            DisplayError();
        }
    }
    public void CombatOnDoubleMovement()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.CombatDoubleMove();
        }
        else
        {
            DisplayError();
        }
    }

 

    void CombatSendLocation(RaycastData ray, CombatManager tempref)
    {
        if (ray.HitBool && VerifyTag(ray, "Terrain"))
        {
            tempref.CombatMovement(ray.Hit.point);
        }
    }

    void CombatSendTarget(RaycastData ray, CombatManager tempRef)
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
    #endregion
    #region RoamingMethods

    void RoamingOnSelect()
    {
        RaycastData ray = GetRaycastHit();
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Roaming)
        {
            Debug.Log(ray.Hit.point);
            RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
            if (ray.HitBool && VerifyTag(ray, "Terrain"))
            {
                tempRef.MoveToLocation(ray.Hit.point);
            }
            else if(ray.HitBool && VerifyTag(ray, "Interactable") && ray.Hit.transform.GetComponent<GameObject>() != null)
            {
                tempRef.MoveAndInteract(ray.Hit.point, ray.Hit.transform.GetComponent<GameObject>());
            }
            //code to determine if can travel a path vs travel then interact vs dont travel
        }
        else
        {
            DisplayError();
        }
    }

    void RoamingInteract()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Roaming)
        {
            RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.Interact();
        }
        else
        {
            DisplayError();
        }

    }
    void RoamingInventory()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Roaming)
        {
            RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.OpenInventory();
        }
        else
        {
            DisplayError();
        }
    }


    /*void RoamingMovement(InputAction inputAction)
    {
    Vector2 movement = inputAction.ReadValue<Vector2>();
    Debug.Log(movement);
    }*/


    #endregion

}
