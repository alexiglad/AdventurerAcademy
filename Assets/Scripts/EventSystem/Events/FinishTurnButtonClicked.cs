using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTurnButtonClicked : MonoBehaviour
{
    //these are created programmatically for each button 
#pragma warning disable
    public event EventHandler OnFinishTurnButtonClicked;
#pragma warning restore


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))//TODO temporary need to add finish turn button
        {
            //left click pressed
            OnFinishTurnButtonClicked?.Invoke(this, EventArgs.Empty);
        }*/
    }
}
