using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "ScriptableObjects/InputHandler")]
public class InputHandler : ScriptableObject
{
    Camera defaultCamera;
    Camera activeCamera;
    Controls controls;
    Vector2 pan;
    float zoom;

    [SerializeField] GameStateManagerSO gameStateManager;

    public Camera ActiveCamera { get => activeCamera;}
    public Vector2 Pan { get => pan;}
    public float Zoom { get => zoom;}

    public void ManualAwake()
    {
        controls = new Controls();
        controls.Combat.Select.performed += _ => OnSelect();
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
        Vector2 mousePosition = controls.Combat.MousePosition.ReadValue<Vector2>();
        Ray ray = activeCamera.ScreenPointToRay(mousePosition);
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempref = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            if (tempref.GetTargeting() == true)
            {
                SendTarget(ray, tempref);
            }
            else
            {
                SendLocation(ray, tempref);
            }
        }
    }

    void SendLocation(Ray ray, CombatManager tempref)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Tag: " + hit.collider.tag);
            if (hit.collider.tag == "Terrain" && !EventSystem.current.IsPointerOverGameObject() && hit.transform != null)
            {
                tempref.CombatMovement(hit.point);                
            }
        }
    }

    void SendTarget(Ray ray, CombatManager tempref)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Tag: " + hit.collider.tag);
            if (hit.collider.tag == "Character" && !EventSystem.current.IsPointerOverGameObject() && hit.transform != null)
            {
                if (hit.transform.GetComponent<Character>() != null)
                {
                    //debugging commented out
                    //Character debug = hit.transform.GetComponent<Character>();
                    //Debug.Log("Ability was selected on: " + debug.GetName());
                    tempref.CombatTarget(hit.transform.GetComponent<Character>());
                }
            }
        }
    }
}
