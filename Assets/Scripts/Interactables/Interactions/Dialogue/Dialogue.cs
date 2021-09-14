using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Interaction/Dialogue")]
public class Dialogue : Interaction
{
    [SerializeField] private TextAsset inkJSONAsset;

    public override void HandleInteraction() 
    {
        Debug.Log("Initiated dialogue here");
        dialogueProcessor.HandleDialogue(inkJSONAsset);
    }
}
