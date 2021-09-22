using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public abstract class SceneSO : ScriptableObject
{
    public string sceneName;
    [SerializeField] private GameStateEnum targetGameState;
    [SerializeField] Vector3[] characterPositions;

}
