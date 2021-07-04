using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Heals selected target by 5 hp
/// and has a 50% chance of giving that 
/// target regeneraton (for 2 hp) 
/// for 3 turns
/// 
/// 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/MagicalHeal")]
public class MagicalHeal : Ability
{
    private FloatValueSO damage;
    private FloatValueSO regen;
#pragma warning disable
    private AbilityTypeEnum abilityType;
#pragma warning restore
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);
        regen = (FloatValueSO)CreateInstance("FloatValueSO");
        regen.SetFloatValue(2f);
        abilityType = AbilityTypeEnum.Heal;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Heal(attackee, damage.GetFloatValue());

        if (Random.value >= 0.5)//give target regen
        {
            Status status = new Status(regen.GetFloatValue(), StatusTypeEnum.Regen, 3);
            statusProcessor.CreateStatus(attacker, attackee, status);
        }
    }
}
