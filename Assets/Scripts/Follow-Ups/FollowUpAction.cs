using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpAction
{
    Ability ability;
    Vector3 direction;//TODO determine how to implement this
    FollowUp followUp;
    StatusTypeEnum statusEffect;

    FollowUpActionTypeEnum followUpActionType;

    Character attacker;
    Character attackee;

    public FollowUpAction(Character attacker, Character attackee, Ability ability)
    {
        this.attacker = attacker;
        this.attackee = attackee;
        this.ability = ability;
        followUpActionType = FollowUpActionTypeEnum.Ability;
    }

    public FollowUpAction(Character attacker, Vector3 direction)
    {
        this.attacker = attacker;
        this.direction = direction;

        followUpActionType = FollowUpActionTypeEnum.Movement;
    }

    public FollowUpAction(Character attacker, Character attackee, FollowUp individualFollowUp)
    {
        this.attacker = attacker;
        this.attackee = attackee;
        this.followUp = individualFollowUp;

        followUpActionType = FollowUpActionTypeEnum.FollowUp;
    }

    public FollowUpAction(Character attacker, Character attackee, StatusTypeEnum statusEffect)
    {
        this.statusEffect = statusEffect;
        this.attacker = attacker;
        this.attackee = attackee;

        followUpActionType = FollowUpActionTypeEnum.Status;
    }

    public Ability Ability { get => ability; set => ability = value; }
    public Vector2 Movement { get => direction; set => direction = value; }
    public FollowUp IndividualFollowUp { get => followUp; set => followUp = value; }
    public Character Attacker { get => attacker; set => attacker = value; }
    public Character Attackee { get => attackee; set => attackee = value; }
    public StatusTypeEnum StatusEffect { get => statusEffect; set => statusEffect = value; }
    public FollowUpActionTypeEnum FollowUpActionType { get => followUpActionType; set => followUpActionType = value; }
}