using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateEnum targetGameState;

    [SerializeField] UIHandler uiHandler;
    [SerializeField] GameControllerSO gameController;
    [SerializeField] InputHandler controls;
    [SerializeField] CharacterListSO characterList;
    [SerializeField] Vector3[] characterPositions;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject[] playerPrefabs;//this stores all the prefabs for all characters to create dynamically

    #region gamecontroller basic methods
    void OnEnable()
    {
        gameController.SetGameController(this);

        controls.ManualAwake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        if (characterPositions.Length == 0)
        {
            Debug.Log("ERROR PLEASE ADD POSITIONS");
            currentGameStateManager.CreateStateInstance(targetGameState, characterList.GetCharacters());//actual code for release
            return;
        }
        int pos = 0;
        if (characterPositions.Length == 0)
        {
            Debug.Log("please add positions on gamecontroller for characters to spawn");
        }
        foreach (CharacterIDEnum name in playerData.MissionCharacters)
        {
            if (pos >= characterPositions.Length)
            {
                Debug.Log("possible error tried to add to many characters to given scene");
            }
            foreach (GameObject prefab in playerPrefabs)
            {
                if (prefab.GetComponent<Player>().CharacterID == name)
                {
                    GameObject go = Instantiate(prefab, characterPositions[pos], Quaternion.identity);
                    Character character = go.GetComponent<Character>();
                    characterList.AddCharacter(character);
                    character.ManualAwake();
                    break;//breaks out of currrent foreach loop bc found equivalent prefab
                }
            }
            pos++;
        }

        currentGameStateManager.CreateStateInstance(targetGameState, characterList.GetCharacters());


    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        Destroy(this);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {//todo start coroutine here to fade in screen

        controls.GetControls().Enable();
    }
    void OnSceneUnloaded(Scene scene)
    {
        controls.GetControls().Disable();
    }
    #endregion

    #region coroutines
    public void StartCoroutineTime(float time, Action action)
    {
        StartCoroutine(Routine(time, action));
    }
    IEnumerator Routine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
    public void StartCoroutineCC(Action action)
    {
        StartCoroutine(Routine1(action));
    }
    IEnumerator Routine1(Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            action.Invoke();
        }
        else if(currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(RoamingManager))
        {
            RoamingManager tempRef = (RoamingManager)currentGameStateManager.GetCurrentGameStateManager();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            action.Invoke();
        }
        else
        {
            Debug.Log("error");
        }
    }
    public void StartCoroutineNMA(Action action, List<Character> turnOrder)
    {
        StartCoroutine(Routine2(action, turnOrder));
    }
    IEnumerator Routine2(Action action, List<Character> turnOrder)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            tempRef.DisableCombatInput();
            uiHandler.UpdateCombatTurnUI(tempRef.Character);

            //yield return new WaitForSeconds(.01f);
            uiHandler.StopDisplayingAbilities();
            uiHandler.StopDisplayingEndTurn();
            uiHandler.UpdateTurnOrder(turnOrder);
            yield return new WaitUntil(tempRef.CanContinueMethod);
            //uiHandler.UpdateCombatTurnUI(tempRef.Character);
            tempRef.EnableCombatInput();
            action.Invoke();
            //eventually add animation here for switching turns
        }
        else
        {
            Debug.Log("error");
        }
    }
    public void StartCoroutineTOS(float time, Action action)
    {
        StartCoroutine(Routine3(time, action));
    }
    IEnumerator Routine3(float time, Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            yield return new WaitForSeconds(time);
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            action.Invoke();
            tempRef.DisableCombatInput();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            tempRef.EnableCombatInput();
        }
        else
        {
            Debug.Log("error");
        }
    }
    public void StartCoroutineNMAGravity(Action action, SortedSet<Character> characters)
    {
        StartCoroutine(Routine4(action, characters));
    }
    IEnumerator Routine4(Action action, SortedSet<Character> characters)
    {
        foreach (Character character in characters)
        {
            character.Obstacle.enabled = false;
        }
        yield return new WaitForSeconds(0.25f);
        foreach (Character character in characters)
        {
            character.Agent.enabled = true;
        }
        yield return new WaitForSeconds(0.25f);
        foreach (Character character in characters)
        {
            character.Agent.enabled = false;
            character.Obstacle.enabled = true;
        }
        action.Invoke();
    }
    #endregion methods
}
