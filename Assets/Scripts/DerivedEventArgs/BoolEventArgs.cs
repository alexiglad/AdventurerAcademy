using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoolEventArgs : EventArgs
{
    public BoolEventArgs(bool newBool)
    {
        NewBool = newBool; 
    }
    public bool NewBool { get; }

}
