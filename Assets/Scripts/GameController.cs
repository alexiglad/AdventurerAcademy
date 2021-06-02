using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
     * delete create instance code in current managers/processors eventually
     * 
     * 
     */

    /* enum data
     * 
    Combat,
    Dialogue,
    Roaming,
    Loading,
    Menu
     */

    //[SerializeField] public static GameStateManager currentGameStateManager;//Alexi Code
    //[SerializeField] public static GameStateEnum currentGameState;//Alexi Code
    [SerializeField] private GameStateManagerSO currentGameStateManager;//Cedric Change
    [SerializeField] private GameStateSO currentGameState;//Cedric Change


    void Start()
    {
        //instantiate all processor instances!! 
        //and make sure to delete current instantiations

        //temporary code creates combat manager with characters

        CreateStateInstance(GameStateEnum.Combat, null);
        //temporary default
    

    }

    // Update is called once per frame
    void Update()
    {
        //currentGameStateManager.Update();
        //anything else we need
    }


    public void CreateStateInstance(GameStateEnum gameState, SortedSet<Character> characters)
    {
        //TODO
        //need to add code to get data about the current state (such as characters and whatnot)
        //in order to appropriately create the state
        currentGameState.SetGameState(gameState);
        currentGameStateManager.SetGameStateManager(Type.GetType(gameState.ToString() + "Manager"));//Cedric Change
        currentGameStateManager.GetGameStateManager().AddCharacters(characters);//Cedric Change

        /* Alexi Code
        switch (gameState)
        {
            //combat case
            case GameStateEnum.Combat:
                //need to create an instance of a combat manager
                //currentGameStateManager = (CombatManager)CombatManager.CreateInstance("CombatManager");
                //currentGameStateManager.AddCharacters(characters);
                break;
            //dialogue case
            case GameStateEnum.Dialogue:

                break;
            //roaming case
            case GameStateEnum.Roaming:

                break;
            //loading case
            case GameStateEnum.Loading:

                break;
            //menu case
            case GameStateEnum.Menu:

                break;
            //default case
            default:

                break;
        }*/


    }
    /*
     * Alexi Code
    public void DestroyStateInstance()
    {
        Destroy(currentGameStateManager);
    }
    */
}
