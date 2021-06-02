using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameStateManager")]
public class GameStateManagerSO : ScriptableObject
{
    private GameStateManager currentGameStateManager;
    public void SetGameStateManager(Type manager)
    {
        Debug.Log(manager);//Debug
        Destroy(currentGameStateManager);
        currentGameStateManager = (GameStateManager)CreateInstance(manager.ToString());
        Debug.Log(currentGameStateManager.ToString());//Debug
    }

    public GameStateManager GetGameStateManager()
    {
        return currentGameStateManager;
    }    
}
