using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
    private SortedSet<Character> characters = new SortedSet<Character>();

    static Controls controls;

    public static Controls Controls { get => controls; set => controls = value; }

    void Awake()
    {
        //instantiate all processor instances!! 
        //instantiate all follow-ups and abilities

        CreateAllProcessorInstances();
        CreateAllFollowUpInstances();
        CreateAllAbilityInstances();

        //controls
        controls = new Controls();



        //temporary code creates combat manager with characters
        currentGameStateManager.CreateStateInstance(GameStateEnum.Combat, characters);
        //temporary default


    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        float movementInput = controls.Roaming.Movement.ReadValue<float>();
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
        //create instances of all processors
        FollowUpProcessor.CreateInstance("FollowUpProcessor");
        AbilityProcessor.CreateInstance("AbilityProcessor");
        MovementProcessor.CreateInstance("MovementProcessor");
        StatusProcessor.CreateInstance("StatusProcessor");
    }
    private void CreateAllAbilityInstances()
    {
        //create instances of all abilities
        Zap.CreateInstance("Zap");
    }
    private void CreateAllFollowUpInstances()
    {
        //create instances of all follow-ups
        Stab.CreateInstance("Stab");
    }
}
