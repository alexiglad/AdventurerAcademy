using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateManager : ScriptableObject
{
    SubstateEnum state;

    public SubstateEnum State { get => state; set => state = value; }

    public abstract void AddCharacters(SortedSet<Character> characters);
    public abstract void Start();

    public abstract void SetSubstateEnum(SubstateEnum state);
}
