using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
    private SortedSet<Character> characters = new SortedSet<Character>();

    [SerializeField] InputHandler controls;
    //static Controls controls;
    //public static Controls Controls { get => controls; set => controls = value; }

    void Awake()
    {
        controls.ManualAwake();
        //temporary code creates combat manager with characters
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

        }
    }
    public void StartCoroutineNMA(Action action)
    {
        StartCoroutine(Routine2(action));
    }
    IEnumerator Routine2(Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            tempRef.CanContinue = false;
            yield return new WaitForSeconds(.25f);
            tempRef.CanContinue = true;
            action.Invoke();
            //eventually add animation here for switching turns
        }
        else
        {

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
