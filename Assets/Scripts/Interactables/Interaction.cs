using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public abstract class Interaction : ScriptableObject
{

    [SerializeField] protected DialogueProcessor dialogueProcessor;


    public virtual void HandleInteraction() { }

}
