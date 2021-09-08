using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Missions/Atlas")]
public class AtlasSO : ScriptableObject
{
    [SerializeField] private MissionDataSO[] atlas;

    public MissionDataSO[] Atlas { get => atlas; set => atlas = value; }

}

