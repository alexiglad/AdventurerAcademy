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
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Peekaboo")]
public class APeekaboo : Ability
{
    Stats statsBoost;


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.BuildStats(attacker, statsBoost);
        attacker.Unstable = true;


    }
}
