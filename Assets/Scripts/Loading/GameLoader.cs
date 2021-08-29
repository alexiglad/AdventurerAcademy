using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerPreferences playerPreferences;
    [SerializeField] GameLoaderSO gameLoader;
    [SerializeField] CharacterListSO characterList;
    [SerializeField] GameControllerSO gameController;
    string scene;
    int pos;//figure out a better way to do this todo

    private void Start()
    {
        gameLoader.SetGameLoader(this);

        //has to dynamically assign player data and player preferences


        //also has to setup everything according to the data in playerdata/playerpreferences


        LoadMissionOnStart();
    }
    public void LoadMissionOnStart()
    {
        if(playerData.CurrentMission != null)
        {
            //load current mission
            //LoadScene(playerData.CurrentMission.name);
            SceneManager.LoadSceneAsync(playerData.CurrentMission.Name);
            SetCurrentScene(playerData.CurrentMission.Name);
        }
        else
        {
            //load default which is home 
            SceneManager.LoadSceneAsync("Home");
            SetCurrentScene("Home");

            //SceneManager.LoadSceneAsync("CombatDemo"); this is for playtesting can load whichever scene you want initially
        }
    }
    public void LoadMission(string mission)
    {
        foreach(MissionDataSO missionn in playerData.Atlas.Atlas)
        {
            if (missionn.Name.Equals(mission))
            {
                playerData.CurrentMission = missionn;
                break;
            }
        }
        LoadScene(mission);
        //SceneManager.LoadSceneAsync(mission);
    }
    public void SetCurrentScene(string sceneName)
    {
        scene = sceneName;
    }
    public void LoadMission(MissionDataSO mission)
    {
        playerData.CurrentMission = mission;
        LoadScene(mission.Name);
        //SceneManager.LoadSceneAsync(mission.Name);
    }
    void LoadScene(string sceneName)
    {
        characterList.ResetList();
        SceneManager.LoadSceneAsync(sceneName);
        //SceneManager.UnloadSceneAsync(scene);
        SetCurrentScene(sceneName);
    }
    public void LoadSceneAfterCombatWin()
    {//needs to check current mission and if it is done then go to map otherwise load next scene in mission
        Debug.Log("here");
        if(playerData.CurrentMission.Subscenes.Length > pos)
        {
            LoadScene(playerData.CurrentMission.Subscenes[pos]);
            pos++;
        }
        else
        {
            FinishMission();
        }
    }
    public void LoadSceneAfterCombatLoss()
    {//needs to go to default place or maybe home? decide at meeting todo and also determine if need to reset pos

    }
    public void FinishMission()
    {
        pos = 0;
        playerData.CurrentMission.MapStatus = MapStatusEnum.Completed;
        UnlockMissions();

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
}
