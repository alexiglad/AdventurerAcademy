using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/InputHandler")]
public class InputHandler : ScriptableObject
{
    Camera defaultCamera;
    Camera activeCamera;
    Controls controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    

    public void ManualAwake()
    {
        controls = new Controls();
        controls.Combat.Select.performed += _ => OnSelect();
        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        activeCamera = defaultCamera;
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
        Debug.Log("Running On Select");
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
            if (hit.transform != null)
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
            if (hit.transform != null)
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
