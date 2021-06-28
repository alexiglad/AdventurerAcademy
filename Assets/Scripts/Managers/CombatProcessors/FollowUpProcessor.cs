using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FollowUpProcessor")]

public class FollowUpProcessor : ScriptableObject
{
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public void HandleFollowUpAction(FollowUpAction followUpAction)
    {
        if (gameStateManager.GetGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();
            foreach (Character character in tempRef.Characters)//this is sorted properly on initiative
            {
                foreach (FollowUp followUp in character.followUps)
                {
                    if (followUp.IsValid(followUpAction, character))//only need this for checking otherwise you have the info you need
                    {//this info is deductible from the IsValid method (whether you are using the attacker/attackee inflicting the followUp
                        followUp.HandleFollowUp(followUpAction);
                        return;
                    }
                }
            }
        }
        
    }

}
