using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/GameState/GameStateManager")]
public class GameStateManagerSO : ScriptableObject
{
    [SerializeField] private GameStateSO currentGameState;
    private GameStateManager currentGameStateManager;
    [SerializeField] CombatManager combatManager;
    [SerializeField] RoamingManager roamingManager;
    [SerializeField] LoadingManager loadingManager;
    [SerializeField] GameControllerSO gameController;
    [SerializeField] GameLoader gameLoader;

    public void SetGameStateManager(Type manager)
    {
        Destroy(currentGameStateManager);
        String managerType = manager.ToString();
        switch (managerType)
        {
            case "CombatManager":
                currentGameStateManager = Instantiate(combatManager);
                break;
            case "RoamingManager":
                currentGameStateManager = Instantiate(roamingManager);
                break;
            case "LoadingManager":
                currentGameStateManager = Instantiate(loadingManager);
                break;
            default:
                Debug.Log("error switching game states please investigate");
                break;
        }
        Debug.Log("Switched game state to " + currentGameStateManager.ToString());//Debug
    }

    public void CreateStateInstance(GameStateEnum gameState, SortedSet<Character> characters)
    {
        currentGameState.SetGameState(gameState);
        SetGameStateManager(Type.GetType(gameState.ToString() + "Manager"));
        GetCurrentGameStateManager().AddCharacters(characters);
        GetCurrentGameStateManager().SetSubstateEnum(SubstateEnum.Default);
        gameController.GetGameController().StartCoroutineNMAGravity(GetCurrentGameStateManager().Start, characters);//TODO decide on keeping this
        //GetCurrentGameStateManager().Start();
        //controls.ManualAwake(); 
    }
    public GameStateManager GetCurrentGameStateManager()
    {
        return currentGameStateManager;
    }
    public GameStateEnum GetCurrentGameState()
    {
        return currentGameState.GetGameState();
    }
    public GameController GetGameController()
    {
        return this.gameController.GetGameController();
    }
    public GameLoader GetGameLoader()
    {
        return this.gameLoader;
    }
}
