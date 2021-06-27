using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : ScriptableObject
{
    Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Combat.Select.performed += _ => OnSelect();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnSelect()
    {
        RaycastHit hit; 
        Vector2 mousePosition = controls.Combat.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            if(hit.transform != null)
                if(hit.transform.Ge)
    }
}
