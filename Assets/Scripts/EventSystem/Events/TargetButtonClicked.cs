using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButtonClicked : MonoBehaviour
{
    //these are created programmatically for each button 
#pragma warning disable
    public event EventHandler OnTargetButtonClicked;
#pragma warning restore    
    Character target;

    public Character Target { get => target; set => target = value; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))//TODO temporary needs to get button context
        {
            //left click pressed
            OnTargetButtonClicked?.Invoke(this, EventArgs.Empty);
        }*/
    }
}
