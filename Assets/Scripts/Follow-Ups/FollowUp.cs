using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FollowUp : ScriptableObject
{
    public AbilityProcessor abilityProcessorInstance = (AbilityProcessor)AbilityProcessor.FindObjectOfType(typeof(AbilityProcessor));
    public abstract void HandleFollowUp(FollowUpAction followUpAction);
    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
}
