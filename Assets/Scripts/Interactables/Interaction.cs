using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public abstract class Interaction : ScriptableObject
{



    public virtual void HandleInteraction() { }

}
