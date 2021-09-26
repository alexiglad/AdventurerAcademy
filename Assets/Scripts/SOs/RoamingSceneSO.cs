using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "", menuName = "ScriptableObjects/Scene/Roaming")]
public class RoamingSceneSO : SceneSO
{
    [SerializeField] List<Interactable> interactions;

    public List<Interactable> Interactions { get => interactions; set => interactions = value; }
}
