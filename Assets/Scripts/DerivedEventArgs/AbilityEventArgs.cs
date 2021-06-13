using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityEventArgs : EventArgs
{
    public AbilityEventArgs(Ability ability)
    {
        NewAbility = ability;
    }
    public Ability NewAbility { get; }

}
