using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Ability : ScriptableObject
{
    public AbilityProcessor abilityProcessorInstance = (AbilityProcessor) AbilityProcessor.FindObjectOfType(typeof(AbilityProcessor));

    public abstract void HandleAbility(Character attacker, Character attackee, Ability ability);

}
