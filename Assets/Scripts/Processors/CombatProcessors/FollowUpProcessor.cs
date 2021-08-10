using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Processors/FollowUpProcessor")]

public class FollowUpProcessor : ScriptableObject
{
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public void HandleFollowUpAction(FollowUpAction followUpAction)
    {
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            foreach (Character character in tempRef.Characters)//this is sorted properly on initiative
            {
                foreach (FollowUp followUp in character.GetCharacterData().GetFollowUps())
                {
                    if (followUp.IsValid(followUpAction, character))//only need this for checking otherwise you have the info you need
                    {//this info is deductible from the IsValid method (whether you are using the attacker/attackee inflicting the followUp
                        Debug.Log("Follow Up triggered: " + followUp);
                        //TODO add code with game controller coroutine
                        tempRef.AddFollowUp(new FollowUpData(followUp, followUpAction));
                        followUp.HandleFollowUp(followUpAction);
                        return;
                    }
                }
            }
        }
        
    }

}
