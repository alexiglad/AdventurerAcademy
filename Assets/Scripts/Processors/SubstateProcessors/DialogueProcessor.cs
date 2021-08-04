using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "ScriptableObjects/Processors/DialogueProcessor")]
public class DialogueProcessor : ScriptableObject
{
    [SerializeField] GameObject prefab;
    DialogueHandler dialogueHandler;
    public void HandleDialogue(TextAsset dialogue)
    {

        dialogueHandler = Instantiate(prefab).GetComponent<DialogueHandler>();
        dialogueHandler.StartStory(dialogue);
    }
    public void DisableDialogue()
    {
        Destroy(dialogueHandler.gameObject);
        //do other stuff with roaming/combat going back to normal
    }
    
}
