using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Missions/Atlas")]
public class AtlasSO : ScriptableObject
{
    [SerializeField] private MissionDataSO[] atlas;

    public MissionDataSO[] Atlas { get => atlas; set => atlas = value; }
    public AtlasData PackData()
    {
        AtlasData atlasData = new AtlasData(atlas.Length);
        //TODO
        for(int i = 0; i < atlas.Length; i++)
        {
            atlasData.atlas[i] = atlas[i].PackData();
        }
        return atlasData;
    }
}

