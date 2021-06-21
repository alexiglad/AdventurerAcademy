using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Ability")]
public abstract class Ability : ScriptableObject
{
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;

    public void LoadResources()
    {
        Debug.Log("ran");
        abilityProcessor = Resources.FindObjectsOfTypeAll<AbilityProcessor>()[0];
        statusProcessor = Resources.FindObjectsOfTypeAll<StatusProcessor>()[0];
    }

    public abstract void HandleAbility(Character attacker, Character attackee, Ability ability);

}
