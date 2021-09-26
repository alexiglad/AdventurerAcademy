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

    public GameStateEnum TargetGameState { get => targetGameState; set => targetGameState = value; }
    public Vector3[] CharacterPositions { get => characterPositions; set => characterPositions = value; }

    public SceneData PackData()
    {
        SceneData sceneData;
        if(this.GetType() == typeof(RoamingSceneSO))
        {
            RoamingSceneSO roamingScene = (RoamingSceneSO)this;
            sceneData = new SceneData(targetGameState, characterPositions.Length, roamingScene.Interactions);
        }
        else
        {
            sceneData = new SceneData(targetGameState, characterPositions.Length);
        }
        for(int i = 0; i<characterPositions.Length; i++){
            for(int j = 0; j<3; j++)
            {
                sceneData.characterPositions[3*i + j] = this.characterPositions[i][j];
                //todo test this
            }
        }

        return sceneData;
    }
}
