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
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/WingedThrash")]
public class AWingedThrash : Ability
{
    void OnEnable()
    {
        damage = 5;
        range = 4;
        radius = 1.5f;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.SplashDamage(attacker, attackee, damage, range, radius);


    }
}
