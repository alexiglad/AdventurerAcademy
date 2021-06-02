using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpAction
{
    Ability ability;
    Vector2 movement;
    FollowUp individualFollowUp;
    Character attacker;
    Character attackee;

    public FollowUpAction(Character attacker, Character attackee, Ability ability)
    {
        this.attacker = attacker;
        this.attackee = attackee;
        this.ability = ability;
    }

    public FollowUpAction(Character attacker, Vector2 movement)
    {
        this.attacker = attacker;
        this.movement = movement;
    }

    public FollowUpAction(Character attacker, Character attackee, FollowUp individualFollowUp)
    {
        this.attacker = attacker;
        this.attackee = attackee;
        this.individualFollowUp = individualFollowUp;
    }

    public Ability Ability { get => ability; set => ability = value; }
    public Vector2 Movement { get => movement; set => movement = value; }
    public FollowUp IndividualFollowUp { get => individualFollowUp; set => individualFollowUp = value; }
    public Character Attacker { get => attacker; set => attacker = value; }
    public Character Attackee { get => attackee; set => attackee = value; }
}