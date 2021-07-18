using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateManager : ScriptableObject
{
    public abstract void AddCharacters(SortedSet<Character> characters);
    public abstract void Start();
}
