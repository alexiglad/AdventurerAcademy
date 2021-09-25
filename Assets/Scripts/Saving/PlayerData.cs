using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Saving/PlayerData")]
public class PlayerData : ScriptableObject
{

    //temp code
    public void OnEnable()
    {
        currentMission = null;
    }
    //this data is data specific to a particular game/load
    //thus, this data is based on the user and the particular load chosen


    DifficultyEnum difficulty;//difficulty E/M/H

    List<CharacterData> characterDataList;//this stores all data on their current characters/leveling/abilities etc
    //need default player stats/abilities
    List<Player> playerList;//this stores all interactions from player - player
    //default interactions will all be 0
    List<CharacterIDEnum> missionCharacters;//this stores all characters on a given mission, if there is no mission
    //this should be null



    //do we want to have some data type that keeps track of interactions while roaming
    //this includes villager interactions, chest interactions, etc



    //data type for inventory/inventories

    //default is nothing in inventory


    [SerializeField] AtlasSO atlas;
    //default is nothing completed and 1 mission unlocked at academy
    [SerializeField] MissionDataSO currentMission;


    //data type for determining final outcome of story/interactions
    float endingDeterminer;// default 0


    int daysLeft;// default 400?

    public DifficultyEnum Difficulty { get => difficulty; set => difficulty = value; }
    public List<CharacterData> CharacterDataList { get => characterDataList; set => characterDataList = value; }
    public List<Player> PlayerList { get => playerList; set => playerList = value; }
    public AtlasSO Atlas { get => atlas; set => atlas = value; }
    public float EndingDeterminer { get => endingDeterminer; set => endingDeterminer = value; }
    public int DaysLeft { get => daysLeft; set => daysLeft = value; }
    public MissionDataSO CurrentMission { get => currentMission; set => currentMission = value; }
    public List<CharacterIDEnum> MissionCharacters { get => missionCharacters; set => missionCharacters = value; }


    public GameData PackData()
    {
        GameData gameData = new GameData();
        gameData.difficulty = difficulty;
        gameData.characterDataList = characterDataList;
        gameData.playerList = playerList;
        gameData.endingDeterminer = endingDeterminer;
        gameData.daysLeft = daysLeft;
        gameData.missionCharacters = missionCharacters;
        gameData.atlasData = atlas.PackData();
        gameData.currentMission = currentMission.PackData();


        return gameData;
    }
}
