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
public class Zap : Ability
{
    private FloatValueSO damage;
    AbilityProcessor abilityProcessorInstance;
    StatusProcessor statusProcessorInstance;
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);
        abilityProcessorInstance = (AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
        statusProcessorInstance = (StatusProcessor)FindObjectOfType(typeof(StatusProcessor));

        //TODO find a better way to do this eventually
    }


    //this is generally how we will format individual ability class
    //in Start set local variables and create instance

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {

        //handle all ability stuff here, including the amount of damage to deal
        //also handle elemental stuff as well as status stuff

        abilityProcessorInstance.Damage(attackee, damage.GetFloatValue());


    }
}
