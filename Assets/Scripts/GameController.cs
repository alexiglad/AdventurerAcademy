using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
    private SortedSet<Character> characters = new SortedSet<Character>();
    [SerializeField] UIHandler uiHandler;

    [SerializeField] InputHandler controls;
    //static Controls controls;
    //public static Controls Controls { get => controls; set => controls = value; }

    void Awake()
    {
        controls.ManualAwake();
        //temporary code creates combat manager with characters
        //currentGameStateManager.CreateStateInstance(GameStateEnum.Roaming, characters);

        currentGameStateManager.CreateStateInstance(GameStateEnum.Combat, characters);      
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
            //eventually add animation here for switching turns
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
    public void StartCoroutineTOS(Action action)
    {
        StartCoroutine(Routine3(action));
    }
    IEnumerator Routine3(Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
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

    public void AddCharacter(Character character)
    {
        characters.Add(character);
        
    }
    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }


}
