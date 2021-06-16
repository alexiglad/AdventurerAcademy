using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Ability : ScriptableObject
{
    [SerializeField] protected AbilityProcessor abilityProcessorInstance;
    [SerializeField] protected StatusProcessor statusProcessorInstance;
    private void OnEnable()
    {
        abilityProcessorInstance = (AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
        statusProcessorInstance = (StatusProcessor)FindObjectOfType(typeof(StatusProcessor));
    }
    public abstract void HandleAbility(Character attacker, Character attackee, Ability ability);

}
