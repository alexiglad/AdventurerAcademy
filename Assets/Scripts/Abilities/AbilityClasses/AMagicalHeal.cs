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
    [SerializeField] private float regen;
    [SerializeField] private float probability;
    // Start is called before the first frame update


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Heal(attackee, damage);

        if (Random.value <= probability)//give target regen
        {
            Status status = new Status(regen, StatusTypeEnum.Regen, 3);
            statusProcessor.CreateStatus(attacker, attackee, status);
        }
    }
}
