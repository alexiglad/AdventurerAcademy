using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FollowUpProcessor")]

public class FollowUpProcessor : ScriptableObject
{
    CombatManager combatManager;
    public void OnEnable()
    {
        combatManager = (CombatManager)FindObjectOfType(typeof(CombatManager));
    }

    public void HandleFollowUpAction(FollowUpAction followUpAction)
    {
        foreach(Character character in combatManager.Characters)//this is sorted properly on initiative
        {
            foreach(FollowUp followUp in character.followUps)
            {
                if(followUp.IsValid(followUpAction, character))//only need this for checking otherwise you have the info you need
                {//this info is deductible from the IsValid method (whether you are using the attacker/attackee inflicting the followUp
                    followUp.HandleFollowUp(followUpAction);
                    return;
                }
            }
        }
    }

}
