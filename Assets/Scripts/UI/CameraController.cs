using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem.Composites;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{    
    [SerializeField] InputHandler controls;
    [SerializeField] CinemachineVirtualCamera vCameraOne;
    [SerializeField] CinemachineVirtualCamera vCameraTwo;
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] float zoomSpeed;
    [SerializeField] float mouseZoomSpeedMultiplier;
    [SerializeField] float maxZoomIn;
    [SerializeField] float maxZoomOut;
    bool disableCameraInput;
    Vector3 newPosition;
    Coroutine coroutinePan;
    void Start()
    {
        disableCameraInput = false;
        newPosition = transform.position;
        foreach(Transform child in transform)
        {
            switch (child.name)
            {
                case "Virtual Camera One":
                    vCameraOne = child.GetComponent<CinemachineVirtualCamera>();
                    break;
                case "Virtual Camera Two":
                    vCameraTwo = child.GetComponent<CinemachineVirtualCamera>();
                    break;
            }
        }
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
    public void PanCamera(Transform target)
    {
        //StopCoroutine(coroutinePan);
        coroutinePan = StartCoroutine(Pan(target));
    }

    IEnumerator Pan(Transform target)
    {
        vCameraTwo.LookAt = target;
        vCameraTwo.Follow = target;
        vCameraTwo.Priority = 100;
        disableCameraInput = true;
        yield return new WaitForSeconds(.8f);
        vCameraOne.LookAt = target;
        vCameraOne.Follow = target;
        vCameraTwo.Priority = 0;
        vCameraOne.LookAt = null;
        vCameraOne.Follow = null;
        vCameraTwo.LookAt = null;
        vCameraTwo.Follow = null;
        disableCameraInput = false;
    }

    void HandleMovementInput()
    {
        if (disableCameraInput)
        {
            return;
        }
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
            if (controls.Zoom > 0 && vCameraOne.m_Lens.OrthographicSize > maxZoomIn)
            {
                vCameraOne.m_Lens.OrthographicSize -= zoomSpeed * mouseZoomSpeedMultiplier;
                vCameraTwo.m_Lens.OrthographicSize -= zoomSpeed * mouseZoomSpeedMultiplier;
            }
            else if (controls.Zoom < 0 && vCameraOne.m_Lens.OrthographicSize < maxZoomOut)
            {
                vCameraOne.m_Lens.OrthographicSize += zoomSpeed * mouseZoomSpeedMultiplier;
                vCameraTwo.m_Lens.OrthographicSize += zoomSpeed * mouseZoomSpeedMultiplier;
            }
        }
        else
        {
            if (controls.Zoom > 0 && vCameraOne.m_Lens.OrthographicSize > maxZoomIn)
            {
                vCameraOne.m_Lens.OrthographicSize -= zoomSpeed;
                vCameraTwo.m_Lens.OrthographicSize -= zoomSpeed;
            }
            else if (controls.Zoom < 0 && vCameraOne.m_Lens.OrthographicSize < maxZoomOut)
            {
                vCameraOne.m_Lens.OrthographicSize += zoomSpeed;
                vCameraTwo.m_Lens.OrthographicSize += zoomSpeed;
            }
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime);
        //controls.ActiveCamera.transform.localPosition = Vector3.Lerp(controls.ActiveCamera.transform.localPosition, newZoom, movementTime);
    }
}
