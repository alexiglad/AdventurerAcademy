using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Interaction/Dialogue")]
public class Dialogue : Interaction
{
    [SerializeField] private TextAsset inkJSONAsset;

    public override void HandleInteraction() 
    {
        dialogueProcessor.HandleDialogue(inkJSONAsset);
    }
}
