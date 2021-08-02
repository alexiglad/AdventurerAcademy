using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Managers/RoamingManager")]
public class RoamingManager : GameStateManager
{
    private SortedSet<Character> characters = new SortedSet<Character>();
    private IEnumerator<Character> enumerator;
    private Character character;
    private List<GameObject> interactables;
    private bool canContinue;

    [SerializeField] MovementProcessor movementProcessor;
    [SerializeField] UIHandler uiHandler;
    GameController gameController;
    

    public Character Character { get => character; set => character = value; }
    public SortedSet<Character> Characters { get => characters; set => characters = value; }

    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
        character = characters.Min;
        character.Obstacle.enabled = false;
        character.Agent.enabled = true;
    }
    public override void SetSubstateEnum(SubstateEnum state)
    {
        this.State = state;
    }

    public override void Start()
    {
        gameController = FindObjectOfType<GameController>();
        character.Obstacle.enabled = false;
        character.Agent.enabled = true;
        canContinue = true;
        //enable roaming ui
    }


    public void MoveToLocation(Vector3 pos)
    {
        movementProcessor.HandleMovement(character, pos - character.transform.position);
    }
    public void MoveAndInteract(Vector3 pos, Interactable interactable)
    {
        DisableRoamingInput();
        movementProcessor.HandleMovement(character, pos - character.transform.position);
        Action action = () => Interact(interactable);
        gameController.StartCoroutineCC(action);
    }
    public void Interact(Interactable interactable)
    {
        interactable.HandleInteraction();
    }
    public void OpenInventory()
    {
        //TODO implement display UI for everything the character has
    }
    public bool CanContinueMethod()
    {
        return canContinue;
    }
    public void EnableRoamingInput()
    {
        canContinue = true;
    }
    public void DisableRoamingInput()
    {
        canContinue = false;
    }
}
