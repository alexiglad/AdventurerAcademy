using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem.Composites;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float movementTime;
    [SerializeField] Vector3 newPosition;
    [SerializeField] InputHandler controls;
    [SerializeField] Vector3 zoomAmmountOne;
    [SerializeField] Vector3 zoomAmmountTwo;
    [SerializeField] Vector3 newZoom;
    void Start()
    {
        newPosition = transform.position;
        newZoom = controls.ActiveCamera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        controls.SetPan();
        controls.SetZoom();
    }

    void HandleMovementInput()
    {
        if (controls.Pan.y >= 0)
            newPosition += (transform.forward * movementSpeed);
        if (controls.Pan.x <= 0)
            newPosition += (transform.right * -movementSpeed);
        if (controls.Pan.x >= 0)
            newPosition += (transform.right * movementSpeed);
        if (controls.Pan.y <= 0)
            newPosition += (transform.forward * -movementSpeed);

        if (controls.Zoom == 120)
            newZoom -= zoomAmmountOne;
        if (controls.Zoom == -120)
            newZoom += zoomAmmountOne;
        if (controls.Zoom == 1)
            newZoom += zoomAmmountTwo;
        if (controls.Zoom == -1)
            newZoom -= zoomAmmountTwo;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        controls.ActiveCamera.transform.localPosition = Vector3.Lerp(controls.ActiveCamera.transform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
