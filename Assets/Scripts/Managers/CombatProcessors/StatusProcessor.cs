using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatusProcessor")]

public class StatusProcessor : ScriptableObject
{
    [SerializeField] protected GameStateManagerSO gameStateManager;
    [SerializeField] FollowUpProcessor followUpProcessorInstance;



    public void HandleStatuses(Character character)
    {
        foreach (Status status in character.Statuses) {
            status.TurnsLeft--;
            if (status.StatusEffect == StatusTypeEnum.Regen)
            {
                Heal(character, status);
            }
            else if (status.StatusEffect == StatusTypeEnum.Burn ||
                status.StatusEffect == StatusTypeEnum.Poison ||
                status.StatusEffect == StatusTypeEnum.Frozen)
            {
                Damage(character, status);
            }
            else if (status.StatusEffect == StatusTypeEnum.Sleep)
            {
                CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();
                tempRef.IterateCharacters();
            }


            
        }
        for(int i =0; i< character.Statuses.Count; i++)
        {
            if (character.Statuses[i].TurnsLeft < 1)//i.e. status should disappear
            {
                character.Statuses.Remove(character.Statuses[i]);//TODO cannot edit loop while in loop baka
            }
        }
    }
    public void CreateStatus(Character attacker, Character attackee, Status status)
    {
        if (attackee.Statuses.Contains(status))
        {//already has status just update damage and duration if necessary
            int num = attackee.Statuses.IndexOf(status);
            if (attackee.Statuses[num].StatusDamage >= status.StatusDamage && attackee.Statuses[num].TurnsLeft >= status.TurnsLeft)
            {
                //do nothing
            }
            else if (attackee.Statuses[num].StatusDamage >= status.StatusDamage && attackee.Statuses[num].TurnsLeft < status.TurnsLeft)
            {
                attackee.Statuses[num].TurnsLeft = status.TurnsLeft;
            }
            else if(attackee.Statuses[num].StatusDamage < status.StatusDamage && attackee.Statuses[num].TurnsLeft >= status.TurnsLeft)
            {
                attackee.Statuses[num].StatusDamage = status.StatusDamage;
            }
            else//both are less
            {
                attackee.Statuses[num].TurnsLeft = status.TurnsLeft;
                attackee.Statuses[num].StatusDamage = status.StatusDamage;
            }
        }
        else
        {
            attackee.Statuses.Add(status);
        }
        followUpProcessorInstance.HandleFollowUpAction(new FollowUpAction(attacker, attackee, status.StatusEffect));

        //handle animation
    }
    public void Heal(Character character, Status status)
    {
        character.IncrementHealth(status.StatusDamage);
    }
    public void Damage(Character character, Status status)
    {
        character.DecrementHealth(status.StatusDamage);
    }

}
