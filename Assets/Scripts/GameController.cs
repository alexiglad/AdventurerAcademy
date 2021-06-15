using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
    private SortedSet<Character> characters = new SortedSet<Character>();


    void Start()
    {
        //instantiate all processor instances!! 
        //instantiate all follow-ups and abilities

        CreateAllProcessorInstances();
        CreateAllFollowUpInstances();
        CreateAllAbilityInstances();



        //temporary code creates combat manager with characters
        currentGameStateManager.CreateStateInstance(GameStateEnum.Combat, characters);
        //temporary default


    }

    
    // Update is called once per frame
    void Update()
    {
        
        //currentGameStateManager.GetGameStateManager().Update();
        //commented out because combat manager is not completely implemented yet
    }


    /*public void CreateStateInstance(GameStateEnum gameState, SortedSet<Character> characters)
    {
        currentGameState.SetGameState(gameState);
        currentGameStateManager.SetGameStateManager(Type.GetType(gameState.ToString() + "Manager"));
        currentGameStateManager.GetGameStateManager().AddCharacters(characters);
    }*/
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
