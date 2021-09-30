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
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/MarselSlam")]
public class MarselSlam : Ability
{
    void OnEnable()
    {
        damage = 5;
        range = 5;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Damage(attackee, damage);

    }
}
