using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RoamingManager")]
public class RoamingManager : GameStateManager
{
    public SortedSet<Character> characters = new SortedSet<Character>();
    private IEnumerator<Character> enumerator;
    private Character character;
    private List<GameObject> interactables;
    [SerializeField] MovementProcessor movementProcessor;
    GameController gameController;

    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
        this.character = characters.Min;
        character.Obstacle.enabled = false;
        character.Agent.enabled = true;
    }
    public override void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }


    public void MoveToLocation(Vector3 pos)
    {
        movementProcessor.HandleMovement(character, pos - character.transform.position);
    }
    public void MoveAndInteract(Vector3 pos, GameObject obj)
    {

    }
    public void Interact()
    {
        
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
