using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MenuManager")]
public class MenuManager : GameStateManager
{
    public SortedSet<Character> characters = new SortedSet<Character>();
    private IEnumerator<Character> enumerator;
    public void Update()
    {

    }
    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
    }
    public override void Start()
    {

    }
}
