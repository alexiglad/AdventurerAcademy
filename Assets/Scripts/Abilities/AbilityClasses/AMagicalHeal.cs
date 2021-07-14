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
public class AMagicalHeal : Ability
{
    private float regen;
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = 5;
        range = 8;
        regen = 2;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Heal(attackee, damage);

        if (Random.value >= 0.5)//give target regen
        {
            Status status = new Status(regen, StatusTypeEnum.Regen, 3);
            statusProcessor.CreateStatus(attacker, attackee, status);
        }
    }
}
