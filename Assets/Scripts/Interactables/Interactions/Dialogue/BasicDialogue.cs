using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Interaction/BasicDialogue")]
public class BasicDialogue : Interaction
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }
    public override void HandleInteraction() 
    {
        Debug.Log("Initiated basic dialogue here");
    }
}
