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
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);
        regen = (FloatValueSO)CreateInstance("FloatValueSO");
        regen.SetFloatValue(2f);



    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessorInstance.Heal(attackee, damage.GetFloatValue());

        if (Random.value >= 0.5)//give target regen
        {
            Status status = new Status(regen.GetFloatValue(), StatusTypeEnum.Regen, 3);
            statusProcessorInstance.CreateStatus(attacker, attackee, status);
        }
    }
}
