using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    float statusDamage;
    AbilityStatuses statusEffect;//all
    int turnsLeft;//all

    public Status(AbilityStatuses statusEffect, int turnsLeft)
    {
        this.statusEffect = statusEffect;
        this.turnsLeft = turnsLeft;
    }

    public Status(float statusDamage, AbilityStatuses statusEffect, int turnsLeft)
    {
        this.statusDamage = statusDamage;
        this.statusEffect = statusEffect;
        this.turnsLeft = turnsLeft;
    }

    public AbilityStatuses StatusEffect { get => statusEffect; set => statusEffect = value; }
    public int TurnsLeft { get => turnsLeft; set => turnsLeft = value; }
    public float StatusDamage { get => statusDamage; set => statusDamage = value; }

}