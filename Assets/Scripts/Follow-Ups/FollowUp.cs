using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FollowUp : ScriptableObject
{
    public AbilityProcessor abilityProcessorInstance;
    public void Awake()
    {
        abilityProcessorInstance =(AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
    }
    //TODO FIX THIS WONT WORK
    public abstract void HandleFollowUp(FollowUpAction followUpAction);
    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
}
