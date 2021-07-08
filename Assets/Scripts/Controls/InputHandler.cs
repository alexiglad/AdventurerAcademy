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
                SendTarget(GetRaycastHit(), tempref);
            }
            else
            {
                SendLocation(GetRaycastHit(), tempref);
            }
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
        NavMeshHit hit;
        if (ray.HitBool && VerifyTag(ray, "Terrain") && NavMesh.SamplePosition(ray.Hit.point, out hit, 100, -1))
        {
            tempref.CombatMovementTwo(hit.position);
        }
        else
        {
            tempref.CombatMovementTwo(ray.Hit.point);
        }                                      
    }

    void SendTarget(RaycastData ray, CombatManager tempref)
    {
        if (ray.HitBool && VerifyTag(ray, "Character") && ray.Hit.transform.GetComponent<Character>() != null)
            tempref.CombatTarget(ray.Hit.transform.GetComponent<Character>());     
    }
}
