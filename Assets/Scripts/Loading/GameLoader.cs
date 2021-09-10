using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameLoader", menuName = "ScriptableObjects/Loading/GameLoader")]

public class GameLoader : ScriptableObject
{
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerPreferences playerPreferences;
    [SerializeField] CharacterListSO characterList;
    [SerializeField] GameControllerSO gameController;
    string scene;

    public void ManualStart()
    {
        //has to dynamically assign player data and player preferences

        //TEMP code for adding characters... would normally be determined by map and given mission and player choice
        playerData.MissionCharacters.Clear();
        playerData.MissionCharacters.Add(CharacterIDEnum.Lilia);
        playerData.MissionCharacters.Add(CharacterIDEnum.Tengari);

        //also has to setup everything according to the data in playerdata/playerpreferences
        //temp code is to add bird prefab and witch prefab to character list for mission

        LoadMissionOnStart();
    }
    public void LoadMissionOnStart()
    {
        if(playerData.CurrentMission != null)
        {
            //load current mission
            LoadScene(playerData.CurrentMission.name);
        }
        else
        {
            //load default which is home 
            LoadScene("home");
        }
    }
    public void SetCurrentScene(string sceneName)
    {
        scene = sceneName;
    }
    public void LoadMission(string mission)
    {
        foreach(MissionDataSO missionn in playerData.Atlas.Atlas)
        {
            if (missionn.Name.Equals(mission))
            {
                missionn.Pos = 0;//TEMP
                playerData.CurrentMission = missionn;
                LoadScene(missionn.Subscenes[missionn.Pos]);
                break;
            }
        }
    }
    public void LoadMission(MissionDataSO mission)
    {
        playerData.CurrentMission = mission;
        LoadScene(mission.Subscenes[mission.Pos]);
    }
    void LoadScene(string sceneName)
    {
        characterList.ResetList();
        SceneManager.LoadSceneAsync(sceneName);
        //todo figure out how to use code below
        //SceneManager.UnloadSceneAsync(scene);
        //SetCurrentScene(sceneName);
    }
    #region combat/roaming context loading
    public void LoadSceneAfterCombatLoss()
    {//needs to go to default place or maybe home? decide at meeting todo and also determine if need to reset pos
        LoadScene("home");
        //todo implement punishment system here
    }
    public void LoadNextSubscene()
    {//needs to check current mission and if it is done then go to map otherwise load next scene in mission
        Debug.Log("if you get an error here while testing ignore");
        playerData.CurrentMission.Pos++;
        if (playerData.CurrentMission.Subscenes.Length > playerData.CurrentMission.Pos)
        {
            LoadScene(playerData.CurrentMission.Subscenes[playerData.CurrentMission.Pos]);
        }
        else
        {
            FinishMission();
        }
    }
    #endregion
    #region post missions
    public void FinishMission()
    {
        playerData.CurrentMission.MapStatus = MapStatusEnum.Completed;
        UnlockMissions();
        playerData.CurrentMission = null;
        //todo queue map screen here
        Debug.Log("need to queue map");
    }
    void UnlockMissions()//unlocks missions based on current mission being completed
    {
        foreach(string unlocks in playerData.CurrentMission.Unlocks)
        {
            foreach(MissionDataSO mission in playerData.Atlas.Atlas)
            {
                if(mission.Name == unlocks)
                {
                    mission.NumRequisites--;
                    break;
                }
            }
        }
    }
    #endregion
}
