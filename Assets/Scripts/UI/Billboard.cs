using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    
    void LateUpdate()
    {
        //transform.LookAt(inputHandler.ActiveCamera.transform);
        transform.rotation = inputHandler.ActiveCamera.transform.rotation;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
