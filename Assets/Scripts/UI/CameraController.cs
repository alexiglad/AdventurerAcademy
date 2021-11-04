using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem.Composites;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{    
    [SerializeField] InputHandler controls;
    [SerializeField] CinemachineVirtualCamera vCamera;    
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] float zoomSpeed;
    [SerializeField] float mouseZoomSpeedMultiplier;
    [SerializeField] float maxZoomIn;
    [SerializeField] float maxZoomOut;
    Vector3 newPosition;

    void Start()
    {
        newPosition = transform.position;
        vCamera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void FixedUpdate()
    {
        if (controls.GetControls().UniversalControls.enabled)
        {
            HandleMovementInput();
            controls.SetPan();
            controls.SetZoom();
        }
    }

    void HandleMovementInput()
    {
        if (controls.Pan.y > 0)
            newPosition += (transform.forward * movementSpeed);
        if (controls.Pan.x < 0)
            newPosition += (transform.right * -movementSpeed);
        if (controls.Pan.x > 0)
            newPosition += (transform.right * movementSpeed);
        if (controls.Pan.y < 0)
            newPosition += (transform.forward * -movementSpeed);

        if (controls.ZoomContext == "y")
        {
            if (controls.Zoom > 0 && vCamera.m_Lens.OrthographicSize > maxZoomIn)
            {
                vCamera.m_Lens.OrthographicSize -= zoomSpeed * mouseZoomSpeedMultiplier;
            }
            else if (controls.Zoom < 0 && vCamera.m_Lens.OrthographicSize < maxZoomOut)
            {
                vCamera.m_Lens.OrthographicSize += zoomSpeed * mouseZoomSpeedMultiplier;
            }
        }
        else
        {
            if (controls.Zoom > 0 && vCamera.m_Lens.OrthographicSize > maxZoomIn)
            {
                vCamera.m_Lens.OrthographicSize -= zoomSpeed;
            }
            else if (controls.Zoom < 0 && vCamera.m_Lens.OrthographicSize < maxZoomOut)
            {
                vCamera.m_Lens.OrthographicSize += zoomSpeed;
            }
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime);
        //controls.ActiveCamera.transform.localPosition = Vector3.Lerp(controls.ActiveCamera.transform.localPosition, newZoom, movementTime);
    }
}
