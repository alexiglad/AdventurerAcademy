using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// kills current character
/// 
/// 
/// 
/// 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/TEST")]
public class ATEST : Ability
{
    void OnEnable()
    {
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Damage(attacker, attacker.GetHealth());

    }
}
