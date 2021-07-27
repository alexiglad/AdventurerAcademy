using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterList : MonoBehaviour
{
    [SerializeField] List<Character> characters;

    public List<Character> Characters { get => characters; set => characters = value; }
}