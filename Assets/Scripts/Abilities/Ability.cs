using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Ability")]
public class Ability : ScriptableObject
{  
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;

    [SerializeField] protected float damage;
    [SerializeField] protected float range;


    [SerializeField] private AbilityTypeEnum abilityType;
    [SerializeField] private Sprite icon;

    public Sprite Icon { get => icon; set => icon = value; }
    public AbilityTypeEnum AbilityType { get => abilityType; set => abilityType = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }

    private void Awake()
    {
        damage = 0;
        range = 0;

    }

    public virtual void HandleAbility(Character attacker, Character attackee, Ability ability) { }

}
