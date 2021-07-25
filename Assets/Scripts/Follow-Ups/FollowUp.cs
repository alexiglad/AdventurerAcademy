using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FollowUp : ScriptableObject
{
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;
    [SerializeField] protected FollowUpProcessor followUpProcessor;
    [SerializeField] protected FollowUpTypeEnum followUpType;

    public FollowUpTypeEnum FollowUpType => followUpType;

    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
    public abstract void HandleFollowUp(FollowUpAction followUpAction);

    public void FollowUpFollowUp(FollowUpAction followUpAction)
    {
        followUpProcessor.HandleFollowUpAction(new FollowUpAction(followUpAction.Attacker, followUpAction.Attackee, this));
    }
}
