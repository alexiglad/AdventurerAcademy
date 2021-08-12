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
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/SelfKill")]
public class ASelfKill : Ability
{
    void OnEnable()
    {
        //damage = 0;
        //range = 0;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        attacker.SetHealth(0);

    }
}
