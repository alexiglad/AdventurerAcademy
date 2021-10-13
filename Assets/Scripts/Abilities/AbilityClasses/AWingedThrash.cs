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



    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.SplashDamage(attackee, damage, radius);


    }
}
