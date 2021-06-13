using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Bite is a basic monster ability that does a flat 3 damage. 
/// </summary>
public class Bite : Ability
{
    private FloatValueSO damage;
    AbilityProcessor abilityProcessorInstance;
    private void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(3f);
        abilityProcessorInstance = (AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessorInstance.DealDamage(attackee, damage.GetFloatValue());
    }
}
