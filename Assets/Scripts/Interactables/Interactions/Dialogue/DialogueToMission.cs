using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Interaction/DialogueToMission")]
public class DialogueToMission : Interaction
{
    [SerializeField] private TextAsset inkJSONAsset;

    public override void HandleInteraction()
    {
        Debug.Log("Initiated dialogue to mission here");
        gameStateManager.GetGameLoader().LoadMission("Tutorial");
    }
}
