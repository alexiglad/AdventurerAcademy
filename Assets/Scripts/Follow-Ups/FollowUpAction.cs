using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpAction
{
    Ability ability;
    Vector3 direction;
    FollowUp followUp;
    StatusTypeEnum statusEffect;

    FollowUpTypeEnum followUpActionType;

    Character attacker;
    Character attackee;

    public FollowUpAction(Character attacker, Character attackee, Ability ability)
    {
        this.attacker = attacker;
        this.attackee = attackee;
        this.ability = ability;
        followUpActionType = FollowUpTypeEnum.Ability;
    }

    public FollowUpAction(Character attackee, Vector3 direction)
    {
        this.attackee = attackee;
        this.direction = direction;

        followUpActionType = FollowUpTypeEnum.Movement;
    }

    public FollowUpAction(Character attacker, Character attackee, FollowUp individualFollowUp)
    {
        this.attacker = attacker;
        this.attackee = attackee;
        this.followUp = individualFollowUp;

        followUpActionType = FollowUpTypeEnum.FollowUp;
    }

    public FollowUpAction(Character attacker, Character attackee, StatusTypeEnum statusEffect)
    {
        this.statusEffect = statusEffect;
        this.attacker = attacker;
        this.attackee = attackee;

        followUpActionType = FollowUpTypeEnum.Status;
    }
    public FollowUpAction(Character attackee)
    {
        this.attackee = attackee;

        followUpActionType = FollowUpTypeEnum.Death;
    }

    public Ability Ability { get => ability; set => ability = value; }
    public Vector2 Movement { get => direction; set => direction = value; }
    public FollowUp IndividualFollowUp { get => followUp; set => followUp = value; }
    public Character Attacker { get => attacker; set => attacker = value; }
    public Character Attackee { get => attackee; set => attackee = value; }
    public StatusTypeEnum StatusEffect { get => statusEffect; set => statusEffect = value; }
    public FollowUpTypeEnum FollowUpActionType { get => followUpActionType; set => followUpActionType = value; }
}