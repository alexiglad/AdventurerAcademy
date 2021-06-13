using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonClicked : MonoBehaviour
{
    //these are created programmatically for each button 
    public event EventHandler OnAbilityButtonClicked;
    Ability ability;

    public Ability Ability { get => ability; set => ability = value; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//TODO temporary needs to get button context
        {
            //left click pressed
            OnAbilityButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
