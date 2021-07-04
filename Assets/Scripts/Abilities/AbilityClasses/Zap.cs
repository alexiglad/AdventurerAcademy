using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Note: Below is an example ability Summary
/// 
/// 
/// Zap is a basic elemental ability that does a flat 5 damage
/// Zap's Element is Electricity
/// Zap is available to Wizard type characters
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Zap")]
public class Zap : Ability
{
    private FloatValueSO damage;
#pragma warning disable
    private AbilityTypeEnum abilityType;
#pragma warning restore
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);
        abilityType = AbilityTypeEnum.Ranged;
    }


    //this is generally how we will format individual ability class
    //in Start set local variables and create instance

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {

        //handle all ability stuff here, including the amount of damage to deal
        //also handle elemental stuff as well as status stuff

        abilityProcessor.Damage(attackee, damage.GetFloatValue());


    }
}
