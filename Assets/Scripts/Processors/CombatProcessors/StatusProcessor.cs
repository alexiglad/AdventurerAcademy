﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Processors/StatusProcessor")]

public class StatusProcessor : ScriptableObject
{
    [SerializeField] protected GameStateManagerSO gameStateManager;
    [SerializeField] FollowUpProcessor followUpProcessorInstance;



    public void HandleStatuses(Character character)
    {
        if(gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            foreach (Status status in character.Statuses)
            {
                Debug.Log(character + " was affected by " + status.StatusEffect);
                tempRef.AddStatus(new StatusData(status, character));//todo check this works
                if (tempRef.CanContinue)
                {
                    tempRef.EnableCombatInput();
                }
                status.TurnsLeft--;
                if (status.StatusEffect == StatusTypeEnum.Regen)
                {
                    Heal(character, status);
                }
                else if (status.StatusEffect == StatusTypeEnum.Burn ||
                    status.StatusEffect == StatusTypeEnum.Poison ||
                    status.StatusEffect == StatusTypeEnum.Frozen)
                {
                    if (Damage(character, status))
                    {
                        break;
                    }

                }
                else if (status.StatusEffect == StatusTypeEnum.Sleep)
                {
                    tempRef.IterateCharacters();
                }
                else if (status.StatusEffect == StatusTypeEnum.Knocked)
                {
                    //tempRef.Turn.AmountMoved = character.GetMaxMovement();
                    //todo make it so it takes AP to get back up
                }
            }
            CheckStatusesFinished(character, tempRef);
        }
        else
        {
            Debug.Log("error");
            return;
        }

    }
    private void CheckStatusesFinished(Character character, CombatManager tempRef)
    {
        for (int i = 0; i < character.Statuses.Count; i++)
        {
            if (character.Statuses[i].TurnsLeft < 1)//i.e. status should disappear
            {
                tempRef.RemoveStatus(new StatusData(character.Statuses[i], character));
                character.Statuses.Remove(character.Statuses[i]);
                CheckStatusesFinished(character, tempRef);
            }
        }
    }
    public void CreateStatus(Character attacker, Character attackee, Status status)
    {
        bool duplicate = false;
        int pos=0;
        foreach(Status statuss in attackee.Statuses)
        {
            if(statuss.StatusEffect == status.StatusEffect)
            {
                duplicate = true;
                DuplicateStatus(attackee, status, pos);
                break;
                
            }
            pos++;
        }
        if(!duplicate)
        {//i.e. status doesn't already exist
            attackee.Statuses.Add(status);
        }
        if(gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();//todo determine if this should be displayed differently
            tempRef.AddStatus(new StatusData(status, attackee));
            Debug.Log(attackee + " was given " + status.StatusEffect + " effect by " + attacker);
            if (tempRef.CanContinue)
            {
                tempRef.EnableCombatInput();
            }
            followUpProcessorInstance.HandleFollowUpAction(new FollowUpAction(attacker, attackee, status.StatusEffect));
        }

        //handle animation
    }
    public void DuplicateStatus(Character attackee, Status status, int num)
    {
        
        //already has status just update damage and duration if necessary
        if (attackee.Statuses[num].StatusDamage >= status.StatusDamage && attackee.Statuses[num].TurnsLeft >= status.TurnsLeft)
        {
            //do nothing
        }
        else if (attackee.Statuses[num].StatusDamage >= status.StatusDamage && attackee.Statuses[num].TurnsLeft < status.TurnsLeft)
        {
            attackee.Statuses[num].TurnsLeft = status.TurnsLeft;
        }
        else if (attackee.Statuses[num].StatusDamage < status.StatusDamage && attackee.Statuses[num].TurnsLeft >= status.TurnsLeft)
        {
            attackee.Statuses[num].StatusDamage = status.StatusDamage;
        }
        else//both are less
        {
            attackee.Statuses[num].TurnsLeft = status.TurnsLeft;
            attackee.Statuses[num].StatusDamage = status.StatusDamage;
        }
    }
    public void Heal(Character character, Status status)
    {
        character.IncrementHealth(status.StatusDamage);
    }
    public bool Damage(Character character, Status status)
    {
        return character.DecrementHealth(status.StatusDamage);
    }

}
