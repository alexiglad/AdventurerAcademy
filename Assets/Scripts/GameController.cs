using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
    private SortedSet<Character> characters = new SortedSet<Character>();

    [SerializeField] InputHandler controls;
    //static Controls controls;
    AbilityButtonClicked onAbilityButtonClicked;
    //public static Controls Controls { get => controls; set => controls = value; }

    void Awake()
    {
        //instantiate all processor instances!! 
        //instantiate all follow-ups and abilities
        controls.ManualAwake();
        //CreateAllProcessorInstances();

        //Ability.CreateInstance("Ability");
        UIHandler.CreateInstance("UIHandler");

        //temporary code creates combat manager with characters
        currentGameStateManager.CreateStateInstance(GameStateEnum.Combat, characters);
        //controls
        
        /////////////////////////////
        //controls.Combat.Select.started += ctx => 
        //use context to distinguish between movement and a target
        //target is only done when an ability has been selected?
        //this overwrites the movement raycast until either a target has been selected or the user
        //right clicks allowing for moving again and canceling ability
        //controls.Combat.Select
        //
        



    }

    /*
    private void OnEnable()
    {
        controls.GetControls().Enable();
    }

    private void OnDisable()
    {
        controls.GetControls().Disable();
    }
    */

    private void Update()
    {
        float movementInput = controls.GetControls().Roaming.Movement.ReadValue<float>();
        //controls.Combat.
    }


    public void AddCharacter(Character character)
    {
        characters.Add(character);
        
    }
    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }
    private void CreateAllProcessorInstances()
    {
        //Cedric Edit
        //create instances of all processors
        //FollowUpProcessor.CreateInstance("FollowUpProcessor");
        //AbilityProcessor.CreateInstance("AbilityProcessor");
        //MovementProcessor.CreateInstance("MovementProcessor");
        //StatusProcessor.CreateInstance("StatusProcessor");
    }

}
