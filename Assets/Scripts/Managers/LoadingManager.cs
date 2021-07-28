using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LoadingManager")]
public class LoadingManager : GameStateManager
{
    public SortedSet<Character> characters = new SortedSet<Character>();
    private IEnumerator<Character> enumerator;
    public void Update()
    {

    }

    public override void SetSubstateEnum(SubstateEnum state)
    {
        this.State = state;
    }

    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
    }
    public override void Start()
    {

    }
}
