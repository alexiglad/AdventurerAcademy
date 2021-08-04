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
    //Controls controls;
    [SerializeField] InputHandler controls;
    [SerializeField] CombatManager combatManager;
    [SerializeField] RoamingManager roamingManager;
    [SerializeField] LoadingManager loadingManager;
    GameController gameController;
    void OnEnable()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void SetGameStateManager(Type manager)
    {
        Destroy(currentGameStateManager);
        String managerType = manager.ToString();
        switch (managerType)
        {
            case "CombatManager":
                currentGameStateManager = Instantiate(combatManager);
                if(SceneManager.GetActiveScene().name != "CombatDemo")//temp code for demo
                {
                    SceneManager.LoadScene("CombatDemo");
                }
                break;
            case "RoamingManager":
                currentGameStateManager = Instantiate(roamingManager);
                if (SceneManager.GetActiveScene().name != "RoamingDemo")//temp code for demo
                {
                    SceneManager.LoadScene("RoamingDemo");
                }
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

    public GameStateEnum GetCurrentGameState()
    {
        return currentGameState.GetGameState();
    }

    public GameStateManager GetCurrentGameStateManager()
    {
        return currentGameStateManager;
    }
    public void CreateStateInstance(GameStateEnum gameState, SortedSet<Character> characters)
    {
        currentGameState.SetGameState(gameState);
        SetGameStateManager(Type.GetType(gameState.ToString() + "Manager"));
        GetCurrentGameStateManager().AddCharacters(characters);
        GetCurrentGameStateManager().SetSubstateEnum(SubstateEnum.Default);
        gameController = FindObjectOfType<GameController>();
        gameController.StartCoroutineNMAGravity(GetCurrentGameStateManager().Start, characters);//TODO decide on keeping this
        //GetCurrentGameStateManager().Start();
    }
}
