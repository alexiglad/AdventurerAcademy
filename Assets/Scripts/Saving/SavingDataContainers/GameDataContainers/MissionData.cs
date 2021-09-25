using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionData
{
    public MissionData(int unlocksL, int subscenesL)
    {
        endingWeight = new float[3];
        mapLocation = new float[3];
        unlocks = new string[unlocksL];
        subscenes = new SceneData[subscenesL];
    }
    public string name;
    public MapStatusEnum mapStatus;
    public int numRequisites;
    public string[] unlocks;
    public float[] endingWeight;
    public int pos;
    public SceneData[] subscenes;
    public float[] mapLocation;
    public Sprite image;//todo figure out different method of serialization
    public float dimWidth;
    public float dimHeight;
}
