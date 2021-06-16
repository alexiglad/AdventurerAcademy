using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Heals selected target by 5 hp
/// 
/// 
/// 
/// 
/// </summary>
public class MagicalHeal : Ability
{
    private FloatValueSO damage;
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);

    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessorInstance.Heal(attackee, damage.GetFloatValue());


    }
}
