using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/Characters/CharacterList")]
public class CharacterListSO : ScriptableObject
{
    SortedSet<Character> characters;

    public SortedSet<Character> GetCharacters()
    {
        return characters;
    }
    public void AddCharacter(Character character)
    {
        this.characters.Add(character);
    }
    public void ResetList()//todo call this on loading manager
    {
        characters.Clear();
    }
}
