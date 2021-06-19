using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Voodoo allows a caster to place a voodoo doll and bind it to an enemy, allowing abilities and Follow-ups to be used on the 
/// doll with the damage transfering to the target enemy. The doll has a set ammount of HP and
/// cannot be moved once created.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Voodoo")]
public class Voodoo : Ability
{
    private Character target;
    private void OnEnable()
    {

    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {

    }
}