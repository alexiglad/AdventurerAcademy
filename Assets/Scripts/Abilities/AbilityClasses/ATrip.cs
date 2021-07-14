﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Melee range special ability that does a small amount of 
/// fire damage (6) with a chance to knock-down enemy (50%)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Trip")]
public class ATrip : Ability
{
    void OnEnable()
    {
        damage = 6;
        range = 3;

    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Damage(attackee, damage);
        if (Random.value > 0.5)
        {
            Status status = new Status(StatusTypeEnum.Knocked, 1);
            statusProcessor.CreateStatus(attacker, attackee, status);
        }

    }
}
