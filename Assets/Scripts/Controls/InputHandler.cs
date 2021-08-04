using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/Handlers/InputHandler")]
public class InputHandler : ScriptableObject
{
    #region local variables
    Camera defaultCamera;
    Camera activeCamera;
    Controls controls;
    Vector2 pan;
    float zoom;
    [SerializeField] bool initialized;
    [SerializeField] private GameObject tempCharacter;


    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] MovementProcessor movementProcessor;
    [SerializeField] DialogueProcessor dialogueProcessor;


    public Camera ActiveCamera { get => activeCamera;}
    public Vector2 Pan { get => pan;}
    public float Zoom { get => zoom;}
    #endregion
    private void OnEnable()
    {
        initialized = false;
    }
    public void ManualAwake()
    {
        if (!initialized)
        {
            //Debug.Log("Initialized controls: " + Time.realtimeSinceStartup);
            controls = new Controls();
            defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            activeCamera = defaultCamera;

            controls.UniversalControls.Select.performed += _ => OnSelect();
            controls.UniversalControls.Deselect.performed += _ => OnDeselect();
            controls.UniversalControls.Space.performed += _ => OnSpace();
            controls.UniversalControls.DoubleMovement.performed += _ => OnDoubleMovement();
            controls.UniversalControls.Pan.performed += _ => SetPan();
            controls.UniversalControls.Zoom.performed += _ => SetZoom();
            controls.UniversalControls.Interact.performed += _ => OnInteract();
            controls.UniversalControls.Inventory.performed += _ => OnInventoryToggle();
            initialized = true;
        }
        else
        {
            
            defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            activeCamera = defaultCamera;
            //TODO make this less sketch
        }
    }

    #region generalized methods
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
    Vector3 GetLocation(RaycastData ray)
    {
        if (ray.HitBool && VerifyTag(ray, "Terrain"))
        {
            return ray.Hit.point;
        }
        return Vector3.zero;
    }
    void DisplayError()
    {
        Debug.Log("error occurred");
    }
    #endregion
    #region camera controls
    public void SetZoom()
    {
        zoom = controls.UniversalControls.Zoom.ReadValue<float>();
    }

    public void SetPan()
    {
        pan = controls.UniversalControls.Pan.ReadValue<Vector2>();
    }
    #endregion
    #region selection methods
    void OnSelect()
    {
        RaycastData data = GetRaycastHit();
        switch (gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
            {
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
                        if (pos != Vector3.zero)
                        {
                            tempCombatRef.CombatMovement(pos);
                        }
                    }
                }
                break;
            }

            case GameStateEnum.Roaming:
            {
                RoamingManager tempRoamingRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();

                if (data.HitBool && VerifyTag(data, "Terrain"))
                {
                    Vector3 pos = GetLocation(data);
                    if (pos != Vector3.zero)
                    {
                        tempRoamingRef.MoveToLocation(pos);
                    }
                }
                else if (data.HitBool && VerifyTag(data, "Interactable"))
                {
                    if (CanInteract(tempRoamingRef, data))
                    {
                        //just call interact method
                        tempRoamingRef.Interact(data.Hit.transform.GetComponent<Interactable>());
                    }
                    else if (ClickHasInteractable(data))
                    {
                        //click and move
                        tempRoamingRef.MoveAndInteract(data.Hit.point, data.Hit.transform.GetComponent<Interactable>());
                    }
                    else
                    {
                        //just move to location
                        tempRoamingRef.MoveToLocation(data.Hit.point);
                    }
                }
                break;
            }

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
            {
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.CombatAbilityDeselect();
                break;
            }

            default:
            {
                DisplayError();
                break;
            }

        }
    }
    void OnSpace()
    {
        //TEMP CODE USE SUBSTATE IN THE FUTURE
        dialogueProcessor.ProceedDialogue();
    }
    #endregion
    #region combat manager methods

    public void OnDoubleMovement()
    {
        switch (gameStateManager.GetCurrentGameState())
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
                if ((ray.Hit.transform.GetComponent<Character>().IsPlayer() ^ tempRef.Character.IsPlayer()))
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
        //Debug.Log("selected incorreclty")
    }
    #endregion
    #region roaming manager methods
    void OnInteract()
    {
        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Roaming:
            {
                RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
                if (CanInteract(tempRef))
                {
                    tempRef.Interact(GetClosestInteractable(tempRef));
                }
                else
                {

                }
                break;
            }

            default:
            {
                DisplayError();
                break;
            }
        }
    }

    void OnInventoryToggle()
    {
        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Roaming:
            {
                RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.OpenInventory();
                break;
            }

            default:
            {
                DisplayError();
                break;
            }
        }
    }

    bool CanInteract(RoamingManager tempRef, RaycastData ray = null)
    {
        if(ray != null)
        {//use data to get object mouse is pointing to and determine if it is within the character's range
            if (ray.HitBool && VerifyTag(ray, "Interactable") && ray.Hit.transform.GetComponent<Interactable>() != null &&
                tempRef.Character.BoxCollider.bounds.Intersects(ray.Hit.transform.GetComponent<BoxCollider>().bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {//determine if there is an interactable object within collider of character 
            //CardinaDirectionsEnum moveDirection = tempRef.Character.Direction;
            foreach(Interactable interactable in tempRef.Character.InteractablesWithinRange)
            {//TODO eventually implement WASD into this
                if (tempRef.Character.BoxCollider.bounds.Intersects(interactable.GetComponent<BoxCollider>().bounds)){
                    return true;
                }
            }
            return false;
        }
    }
    Interactable GetClosestInteractable(RoamingManager tempRef)
    {
        Interactable returnVal = null;
        float smallestDistance = Mathf.Infinity;
        foreach (Interactable interactable in tempRef.Character.InteractablesWithinRange)
        {
            if (Vector3.Distance(tempRef.Character.BoxCollider.center, interactable.GetComponent<BoxCollider>().center) <= smallestDistance)
            {
                returnVal = interactable; 
            }
        }
        return returnVal;
    }
    bool ClickHasInteractable(RaycastData ray)
    {//todo implement
        return ray.HitBool && VerifyTag(ray, "Interactable") && ray.Hit.transform.GetComponent<Interactable>() != null;
    }

    #endregion
}
