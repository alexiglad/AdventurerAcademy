using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Ability")]
public abstract class Ability : ScriptableObject
{  
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;
    [SerializeField] protected AbilityTypeEnum abilityType;
    [SerializeField] private Sprite icon;

    public Sprite Icon { get => icon; set => icon = value; }

    public abstract void HandleAbility(Character attacker, Character attackee, Ability ability);

}
