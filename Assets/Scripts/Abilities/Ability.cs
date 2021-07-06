using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Ability")]
public abstract class Ability : ScriptableObject
{  
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;
    [SerializeField] protected AbilityTypeEnum abilityType;

    public abstract void HandleAbility(Character attacker, Character attackee, Ability ability);

}
