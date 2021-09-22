using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SaveSystem", menuName = "ScriptableObjects/Saving/SaveSystem")]
public class SaveSystem : ScriptableObject
{
    public GameStateManagerSO gameStateManager;
    public PlayerData playerData;
    public PlayerPreferences playerPreferences;

    string saveFolder;
    private void OnEnable()
    {
        saveFolder = Application.dataPath + "/Saves/";
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }
    public void Save(string fileName)
    {

        SaveData data = new SaveData(this);
        string json0 = JsonUtility.ToJson(data);
        //string 
        //Debug.Log("should have saved");
        Debug.Log(json0);
        //Debug.Log(saveFolder);
        File.WriteAllText(saveFolder + fileName + ".json", json0);

    }
    public void Load(string fileName)
    {

    }
}
