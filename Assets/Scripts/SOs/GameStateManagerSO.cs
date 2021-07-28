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
    [SerializeField] CombatManager combatManager;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] RoamingManager roamingManager;
    [SerializeField] LoadingManager loadingManager;
    [SerializeField] MenuManager menuManager;
    GameController gameController;
    void OnEnable()
    {
        FollowUpProcessor followUpProcessorInstance = (FollowUpProcessor)FindObjectOfType(typeof(FollowUpProcessor));
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
                break;
            case "DialogueManager":
                currentGameStateManager = Instantiate(dialogueManager);
                break;
            case "RoamingManager":
                currentGameStateManager = Instantiate(roamingManager);
                break;
            case "LoadingManager":
                currentGameStateManager = Instantiate(loadingManager);
                break;
            case "MenuManager":
                currentGameStateManager = Instantiate(menuManager);
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
        //gameController = FindObjectOfType<GameController>();
        //gameController.StartCoroutineNMAGravity(GetCurrentGameStateManager().Start, characters);//TODO decide on keeping this
        GetCurrentGameStateManager().SetSubstateEnum(SubstateEnum.Default);
        GetCurrentGameStateManager().Start();

        controls.GetControls().Enable();
    }
}
