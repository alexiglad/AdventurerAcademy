using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //SOs
    GameStateManagerSO gameStateManager;
    PlayerData playerData;
    PlayerPreferences playerPreferences;
    //DATA TO SAVE///////////////////////////////////////////////////

    //manager/state data
    public GameStateEnum gameState;
    public SubstateEnum substate;
    //CMData

    //RMData


    //PPData
    public PPData ppData;
    public List<int> test;


    //PData




    /////////////////////////////////////////////////////////////////
    public SaveData(SaveSystem saveSystem)
    {
        gameStateManager = saveSystem.gameStateManager;
        playerData = saveSystem.playerData;
        playerPreferences = saveSystem.playerPreferences;
        ExtractData();
    }
    void ExtractData()
    {
        gameState = gameStateManager.GetCurrentGameState();
        substate = gameStateManager.GetSubstate();
        ExtractStateData();
        ExtractPreferenceData();
        ExtractPlayerData();
    }
    void ExtractStateData()
    {

        //will call associated packing methods for individual classes and custom data container created for the sole purpose of saving
    }
    void ExtractPreferenceData()
    { 
        test = new List<int>();
        test.Add(1);
        test.Add(2);
        ppData = playerPreferences.PackData();
    }
    void ExtractPlayerData()
    {

    }

}
