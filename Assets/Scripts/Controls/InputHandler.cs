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
        controls.Combat.Select.performed += _ => OnSelect();
        controls.Combat.Deselect.performed += _ => OnDeselect();
        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        activeCamera = defaultCamera;
    }

    public void SetZoom()
    {
        zoom = controls.Combat.Zoom.ReadValue<float>();
    }

    public void SetPan()
    {
        //Debug.Log("Panning");
        pan = controls.Combat.Pan.ReadValue<Vector2>();
    }

    public void SetActiveCamera(Camera active)
    {
        activeCamera = active;
    }
    
    public Controls GetControls()
    {
        return controls;
    }

    void OnSelect()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            if (tempRef.CanContinue)
            {
                if (tempRef.GetTargeting() == true)
                {
                    SendTarget(GetRaycastHit(), tempRef);
                }
                else
                {
                    SendLocation(GetRaycastHit(), tempRef);
                }
            }
        }
    } 
    public void OnDeselect()
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.CombatAbilityDeselect();
        }
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

    void SendLocation(RaycastData ray, CombatManager tempref)
    {
        if (ray.HitBool && VerifyTag(ray, "Terrain"))
        {
            tempref.CombatMovementTwo(ray.Hit.point);
        }
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
                tempRef.CombatTarget(temp1);//TODO CHECK IF THIS WORKS
                Debug.Log("this may cause errors");
                return;
            }
        }
        //returns in every other case where it worked
        Debug.Log("something went wrong or user selected incorrectly");
        //display to user that they are selecting incorrectly   
    }
}
