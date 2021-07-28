using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AtlasSO")]
public class AtlasSO : ScriptableObject  /*SerializableDictionary<string, MissionDataSO>*/
{
    /*[SerializeField] private Dictionary<string, MissionDataSO> atlasDictionary;//[EntrySceneName:RefToMissionDataSO]

    public Dictionary<string, MissionDataSO> AtlasDictionary1 { get => AtlasDictionary; set => AtlasDictionary = value; }*/

    [SerializeField] private SerializableDictionary<string, MissionDataSO> atlasDictionary;

}

