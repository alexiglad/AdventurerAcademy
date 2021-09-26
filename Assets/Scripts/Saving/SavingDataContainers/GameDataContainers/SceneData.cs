using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public GameStateEnum targetGameState;
    public float[] characterPositions;
    List<Interactable> interactions;


    public SceneData(GameStateEnum targetGameState, int length, List<Interactable> interactions = null)
    {
        this.targetGameState = targetGameState;
        characterPositions = new float[3 * length];
        if(targetGameState == GameStateEnum.Combat)
        {
            this.interactions = interactions;
            //dont need any more data as of now
        }
        else
        {
            this.interactions = interactions;//todo test if data in each interaction is packing correctly
        }

    }
}

