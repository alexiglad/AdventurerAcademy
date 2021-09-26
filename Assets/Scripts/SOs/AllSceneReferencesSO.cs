using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Scene/SceneReferences")]
public class AllSceneReferencesSO : ScriptableObject
{
    public SceneSO[] scenes;//add all sceneSOs to this so when testing can use them
}

