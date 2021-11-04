using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem.Composites;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] float globalMaxY;
    [SerializeField] float globalMinY;
    [SerializeField] Vector3 newPosition;
    [SerializeField] InputHandler controls;
    [SerializeField] Vector3 zoomAmmountOne;
    [SerializeField] Vector3 zoomAmmountTwo;
    [SerializeField] Vector3 zoomAmmountPos;
    [SerializeField] Vector3 newZoom;
    [SerializeField] float zoomSpeed;
    [SerializeField] float zoomSpeedMultiplier;

    [SerializeField] CinemachineVirtualCamera vCamera;
    void Start()
    {
        newPosition = transform.position;
        newZoom = controls.ActiveCamera.transform.localPosition;
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
        #region OLD
        /*
        RaycastData hit = controls.GetRaycastHit();
        bool continueZoom = true;
        if (controls.Zoom == 120 )
        {
            if(newZoom.y - zoomAmmountOne.y <= globalMinY)
            {
                continueZoom = false;
            }
        }
        else if (controls.Zoom == -120)
        {
            if (newZoom.y + zoomAmmountOne.y >= globalMaxY)
            {
                continueZoom = false;
            }
        }
        else if (controls.Zoom == 1)
        {
            if (newZoom.y - zoomAmmountTwo.y <= globalMinY)
            {
                continueZoom = false;
            }
        }
        else if (controls.Zoom == -1)
        {
            if (newZoom.y + zoomAmmountTwo.y >= globalMaxY)
            {
                continueZoom = false;
            }
        }
        if (continueZoom && controls.Zoom == 120)
        {
            newZoom -= zoomAmmountOne;
            newPosition += (hit.Hit.point - newPosition) * .15f;
        }
        if (continueZoom && controls.Zoom == -120)
        {
            newZoom += zoomAmmountOne;
            newPosition -= (hit.Hit.point - newPosition) * .15f;
        }
        if (continueZoom && controls.Zoom == 1)
        {
            newZoom -= zoomAmmountTwo;
            newPosition += (hit.Hit.point - newPosition) * .015f;
        }
        if (continueZoom && controls.Zoom == -1)
        {
            newZoom += zoomAmmountTwo;
            newPosition -= (hit.Hit.point - newPosition) * .015f;
        }*/
        #endregion

        if (controls.ZoomContext == "y")
        {
            if (controls.Zoom > 0 && vCamera.m_Lens.OrthographicSize > 1f)
            {
                vCamera.m_Lens.OrthographicSize -= zoomSpeed * zoomSpeedMultiplier;
            }
            else if (controls.Zoom < 0 && vCamera.m_Lens.OrthographicSize < 2.75f)
            {
                vCamera.m_Lens.OrthographicSize += zoomSpeed * zoomSpeedMultiplier;
            }
        }
        else
        {
            if (controls.Zoom > 0 && vCamera.m_Lens.OrthographicSize > 1f)
            {
                vCamera.m_Lens.OrthographicSize -= zoomSpeed;
            }
            else if (controls.Zoom < 0 && vCamera.m_Lens.OrthographicSize < 2.75f)
            {
                vCamera.m_Lens.OrthographicSize += zoomSpeed;
            }
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime);
        //controls.ActiveCamera.transform.localPosition = Vector3.Lerp(controls.ActiveCamera.transform.localPosition, newZoom, movementTime);
    }
}
