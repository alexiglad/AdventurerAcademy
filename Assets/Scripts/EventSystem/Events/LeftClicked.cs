using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClicked : MonoBehaviour
{
    public event EventHandler OnLeftClicked;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //left click pressed
            OnLeftClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
