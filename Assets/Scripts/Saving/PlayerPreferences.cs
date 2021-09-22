using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPreferences", menuName = "ScriptableObjects/Saving/PlayerPreferences")]
public class PlayerPreferences : ScriptableObject
{
    //this class will contain all data regarding the preferences of the player
    //it will remain constant despite what instance of the game the player is in
    //therefore this data is based on the user

    float musicOuput;//range from 0-1
    float sfxOutput;//range from 0-1

    //data type meant for remapping controls/keybinds

    //data type meant for graphics
    bool autosave;//0 is no 1 is yes

    int maxFPS;

    //later have custom data container for switching controls
    private void OnEnable()
    {//temporary code setting it will be set from menu
        musicOuput = .5f;
        sfxOutput = .5f;
        autosave = true;
        maxFPS = 60;
    }
    public PPData PackData()
    {
        PPData ppData = new PPData();
        ppData.musicOuput = musicOuput;
        ppData.sfxOutput = sfxOutput;
        ppData.autosave = autosave;
        ppData.maxFPS = maxFPS;
        return ppData;
    }
}
