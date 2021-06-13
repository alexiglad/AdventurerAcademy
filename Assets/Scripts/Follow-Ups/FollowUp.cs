using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FollowUp : ScriptableObject
{

    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
    public abstract void HandleFollowUp(FollowUpAction followUpAction);

}
