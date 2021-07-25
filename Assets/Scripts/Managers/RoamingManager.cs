using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RoamingManager")]
public class RoamingManager : GameStateManager
{
    public SortedSet<Character> characters = new SortedSet<Character>();
    private IEnumerator<Character> enumerator;
    private Character character;


    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
    }
    public override void Start()
    {
        
    }

    public void Interact()
    {
        Debug.Log("interacted");
        //TODO implement gotta get direction of character and find nearest game object in that direction
    }
    public void OpenInventory()
    {
        //TODO implement display UI for everything the character has
    }
    public void Move(Vector2 movement)
    {
        //character.UpdateMovement(movement);
    }
}
