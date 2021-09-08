using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "ScriptableObjects/Processors/DialogueProcessor")]
public class DialogueProcessor : ScriptableObject
{
    [SerializeField] SubstateSO substate;
    [SerializeField] GameObject prefab;
    DialogueHandler dialogueHandler;
    bool enabled;
    public void HandleDialogue(TextAsset dialogue)
    {
        substate.SetSubstate(SubstateEnum.Dialouge);
        enabled = true;
        dialogueHandler = Instantiate(prefab).GetComponent<DialogueHandler>();
        dialogueHandler.StartStory(dialogue);
    }
    public void DisableDialogue()
    {
        enabled = false;
        substate.SetSubstate(SubstateEnum.Default);
        Destroy(dialogueHandler.gameObject);
        //do other stuff with roaming/combat going back to normal
    }
    public void ProceedDialogue()
    {
        if (enabled)
        {
            dialogueHandler.ProceedDialogue();
        }
    }
    
}
