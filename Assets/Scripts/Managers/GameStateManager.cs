using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateManager : ScriptableObject
{
    //does this class even need anything?
    //make sure to override abstract method
    //TODO
    public abstract void Update();
    public abstract void AddCharacters(SortedSet<Character> characters);
}
