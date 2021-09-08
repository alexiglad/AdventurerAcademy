using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Interaction/BasicDialogue")]
public class BasicDialogue : Interaction
{
    [SerializeField] private TextAsset inkJSONAsset;

    public override void HandleInteraction() 
    {
        Debug.Log("Initiated basic dialogue here");
        dialogueProcessor.HandleDialogue(inkJSONAsset);
    }
}
