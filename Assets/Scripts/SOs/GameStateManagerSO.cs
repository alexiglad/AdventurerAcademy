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
    Controls controls;
    void OnEnable()
    {
        FollowUpProcessor followUpProcessorInstance = (FollowUpProcessor)FindObjectOfType(typeof(FollowUpProcessor));
        controls = GameController.Controls;
    }
    public void SetGameStateManager(Type manager)
    {
        Destroy(currentGameStateManager);
        currentGameStateManager = (GameStateManager)CreateInstance(manager.ToString()); 
        Debug.Log("Switched game state to " + currentGameStateManager.ToString());//Debug
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

        //input system code
        if(gameState == GameStateEnum.Combat)
        {
            controls.Combat.Enable();
        }
        else if (gameState == GameStateEnum.Roaming)
        {
            controls.Roaming.Enable();
        }
        else if (gameState == GameStateEnum.Dialogue)
        {
            controls.Dialogue.Enable();
        }
        else if (gameState == GameStateEnum.Menu)
        {
            controls.Menu.Enable();
        }
        else//this is game state loading
        {
            controls.Disable();
        }
    }
}
