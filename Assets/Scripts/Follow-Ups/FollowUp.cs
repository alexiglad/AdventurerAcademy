using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FollowUp : ScriptableObject
{
    [SerializeField] protected AbilityProcessor abilityProcessorInstance;
    [SerializeField] protected StatusProcessor statusProcessorInstance;
    /*private void OnEnable() //Cedric Edit
    {
        abilityProcessorInstance = (AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
        statusProcessorInstance = (StatusProcessor)FindObjectOfType(typeof(StatusProcessor));
    }*/ //Cedric Edit
    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
    public abstract void HandleFollowUp(FollowUpAction followUpAction);

}
