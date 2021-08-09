using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
    private SortedSet<Character> characters = new SortedSet<Character>();
    [SerializeField] UIHandler uiHandler;

    [SerializeField] InputHandler controls;
    //static Controls controls;
    //public static Controls Controls { get => controls; set => controls = value; }

    void OnEnable()
    {
        //temporary code creates combat manager with characters

        //currentGameStateManager.CreateStateInstance(GameStateEnum.Roaming, characters);//For testing uncoment to switch to roaming
        currentGameStateManager.CreateStateInstance(GameStateEnum.Combat, characters);//For testing uncoment to switch to combat 
        controls.ManualAwake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        Destroy(this);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("enabled " +  this.ToString());
        //Debug.Log("time since startup: " + Time.realtimeSinceStartup);
        controls.GetControls().Enable();
    }
    void OnSceneUnloaded(Scene scene)
    {
        //Debug.Log("disabled " + this.ToString());
        controls.GetControls().Disable();
    }

    #region combat
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
    #endregion methods
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

    public void AddCharacter(Character character)
    {
        characters.Add(character);
        
    }
    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }


}
