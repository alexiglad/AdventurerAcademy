using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public abstract class Interaction : ScriptableObject
{

    [SerializeField] protected DialogueProcessor dialogueProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public virtual void HandleInteraction() { }

}
