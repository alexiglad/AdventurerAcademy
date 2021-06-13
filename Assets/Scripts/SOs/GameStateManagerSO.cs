using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameStateManager")]
public class GameStateManagerSO : ScriptableObject
{
    [SerializeField] private GameStateSO currentGameState;
    private GameStateManager currentGameStateManager;
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
    }
}
