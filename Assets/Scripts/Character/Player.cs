using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<Ability> knownAbilities = new List<Ability>();
    [SerializeField] private CharacterIDEnum characterID;

    public CharacterIDEnum CharacterID { get => characterID; set => characterID = value; }
}