using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatusProcessor")]

public class StatusProcessor : ScriptableObject
{
    CombatManager combatManager;
    FollowUpProcessor followUpProcessorInstance;
    public void OnEnable()
    {
        combatManager = (CombatManager)FindObjectOfType(typeof(CombatManager));
        followUpProcessorInstance = (FollowUpProcessor)FindObjectOfType(typeof(FollowUpProcessor));
    }

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
                combatManager.IterateCharacters();
            }

            //determine if status should fade away
            if (status.TurnsLeft < 1)//i.e. status should disappear
            {
                character.Statuses.Remove(status);
            }
        }
    }
    public void CreateStatus(Character attacker, Character attackee, Status status)
    {
        attackee.Statuses.Add(status);
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
