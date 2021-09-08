using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Characters/CharacterList")]
public class CharacterListSO : ScriptableObject
{
    SortedSet<Character> characters;
    private void OnEnable()
    {
        characters = new SortedSet<Character>();
    }
    public SortedSet<Character> GetCharacters()
    {
        return characters;
    }
    public void AddCharacter(Character character)
    {
        this.characters.Add(character);
    }
    public void ResetList()
    {
        foreach(Character character in characters)
        {
            character.ResetCharacter();
        }
        characters.Clear();
    }
}
