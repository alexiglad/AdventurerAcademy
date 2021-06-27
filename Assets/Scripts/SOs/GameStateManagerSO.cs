using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObjects/GameStateManager")]
public class GameStateManagerSO : ScriptableObject
{
    [SerializeField] private GameStateSO currentGameState;
    private GameStateManager currentGameStateManager;
    //Controls controls;
    [SerializeField] InputHandler controls;
    void OnEnable()
    {
        FollowUpProcessor followUpProcessorInstance = (FollowUpProcessor)FindObjectOfType(typeof(FollowUpProcessor));
        
    }
    public void SetGameStateManager(Type manager)
    {
        Destroy(currentGameStateManager);
        currentGameStateManager = (GameStateManager)CreateInstance(manager.ToString()); 
        Debug.Log("Switched game state to " + currentGameStateManager.ToString());//Debug
    }

    public GameStateEnum GetCurrentGameState()
    {
        return currentGameState.GetGameState();
    }

    public GameStateManager GetGameStateManager()
    {
        return currentGameStateManager;
    }
    public void CreateStateInstance(GameStateEnum gameState, SortedSet<Character> characters)
    {
        currentGameState.SetGameState(gameState);
        SetGameStateManager(Type.GetType(gameState.ToString() + "Manager"));
        GetGameStateManager().AddCharacters(characters);
        GetGameStateManager().Start();

        
        //input system code
        if (gameState == GameStateEnum.Combat)
        {
            controls.GetControls().Combat.Enable();
        }
        else if (gameState == GameStateEnum.Roaming)
        {
            controls.GetControls().Roaming.Enable();
        }
        else if (gameState == GameStateEnum.Dialogue)
        {
            controls.GetControls().Dialogue.Enable();
        }
        else if (gameState == GameStateEnum.Menu)
        {
            controls.GetControls().Menu.Enable();
        }
        else//this is game state loading
        {
            controls.GetControls().Disable();
        }
    }
}
