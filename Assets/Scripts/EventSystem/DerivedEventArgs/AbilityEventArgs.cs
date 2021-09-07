using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityEventArgs : EventArgs
{
    public AbilityEventArgs(Ability ability, bool selected)
    {
        NewAbility = ability;
        Selected = selected;
    }
    public Ability NewAbility { get; }

    public bool Selected { get; }

}
