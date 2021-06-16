using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    float statusDamage;
    StatusTypeEnum statusEffect;//all
    int turnsLeft;//all

    public Status(float statusDamage, StatusTypeEnum statusEffect, int turnsLeft)
    {
        this.statusDamage = statusDamage;
        this.statusEffect = statusEffect;
        this.turnsLeft = turnsLeft;
    }
    public Status(StatusTypeEnum statusEffect, int turnsLeft)
    {
        this.statusEffect = statusEffect;
        this.turnsLeft = turnsLeft;
    }

    

    public StatusTypeEnum StatusEffect { get => statusEffect; set => statusEffect = value; }
    public int TurnsLeft { get => turnsLeft; set => turnsLeft = value; }
    public float StatusDamage { get => statusDamage; set => statusDamage = value; }

}