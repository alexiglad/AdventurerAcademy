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
    //need to incorporate this with save system as follows






    private bool canContinue;

    [SerializeField] MovementProcessor movementProcessor;
    [SerializeField] UIHandler uiHandler;
    [SerializeField] GameStateManagerSO gameStateManager;    

    public Character Character { get => character; set => character = value; }
    public SortedSet<Character> Characters { get => characters; set => characters = value; }

    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
        character = characters.Min;
        if (character != null)
        {
            character.Obstacle.enabled = false;
            character.Agent.enabled = true;
        }
        else
        {
            Debug.Log("error");
        }

    }
    public override void SetSubstateEnum(SubstateEnum state)
    {
        this.State = state;
    }

    public override void Start()
    {
        character.Obstacle.enabled = false;
        character.Agent.enabled = true;
        canContinue = true;
        //enable roaming ui
    }


    public void MoveToLocation(Vector3 pos)
    {
        movementProcessor.HandleMovement(character, pos - CharacterBottom());
    }
    public void MoveAndInteract(Vector3 pos, Interactable interactable)
    {
        DisableRoamingInput();
        movementProcessor.HandleMovement(character, pos - CharacterBottom());
        Action action = () => Interact(interactable);
        gameStateManager.GetGameController().StartCoroutineCC(action);
    }
    Vector3 CharacterBottom()
    {
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        return characterBottom;
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
