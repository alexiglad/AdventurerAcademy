using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AtlasData
{
    public MissionData[] atlas;
    public AtlasData(int length)
    {
        atlas = new MissionData[length];
    }
}
