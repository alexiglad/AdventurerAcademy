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
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);
        abilityProcessorInstance = (AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
        //TODO find a better way to do this eventually
    }


    //this is generally how we will format individual ability class
    //in Start set local variables and create instance

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        //zap is an attack that simply damages the opponent, dealing no splash damage      

        //handle all ability stuff here, including the amount of damage to deal
        //in this case the zap attack (being basic) just deals a constant 3 damage

        abilityProcessorInstance.DealDamage(attackee, damage.GetFloatValue());
        //CombatManagerScriptableObject.combatManagerInstance.characters.Remove(this);		
    }
}
