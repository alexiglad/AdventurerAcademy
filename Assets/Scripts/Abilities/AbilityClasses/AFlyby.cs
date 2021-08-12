using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// 
/// 
/// 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Flyby")]
public class AFlyby : Ability
{
    void OnEnable()
    {
        damage = 10;
        range = 5;
        radius = 1f;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.MovementDamage(attacker, attackee, damage, radius);

    }
}
