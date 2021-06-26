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




    public void OnTargetSelected(Character character)
    {
        target = character;
        OnTargetButtonClicked(this, EventArgs.Empty);
    }

}
