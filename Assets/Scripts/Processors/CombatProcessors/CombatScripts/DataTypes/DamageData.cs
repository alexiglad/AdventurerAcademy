using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData
{
    float healthChange;
    Character character;

    public DamageData(float healthChange, Character character)
    {
        this.healthChange = healthChange;
        this.character = character;
    }

    public float HealthChange { get => healthChange; set => healthChange = value; }
    public Character Character { get => character; set => character = value; }
}