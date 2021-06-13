using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<Ability> knownAbilities = new List<Ability>();

    public Player(string name, float initiative) : base(name, initiative) { }
    
}