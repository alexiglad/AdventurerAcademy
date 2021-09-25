using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public DifficultyEnum difficulty;
    public List<CharacterData> characterDataList;//this stores all data on their current characters/leveling/abilities etc
    //TODO check this
    public List<Player> playerList;
    public List<CharacterIDEnum> missionCharacters;
    public float endingDeterminer;
    public int daysLeft;

    public AtlasData atlasData;
    public MissionData currentMission;
}
