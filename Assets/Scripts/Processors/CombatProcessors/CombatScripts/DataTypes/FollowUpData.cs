using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpData
{
    FollowUp followUp;
    FollowUpAction followUpAction;
    Character character;//this is the character owning the followup

    public FollowUpData(FollowUp followUp, FollowUpAction followUpAction, Character character)
    {
        this.followUp = followUp;
        this.followUpAction = followUpAction;
        this.character = character;
    }

    public FollowUp FollowUp { get => followUp; set => followUp = value; }
    public FollowUpAction FollowUpAction { get => followUpAction; set => followUpAction = value; }
    public Character Character { get => character; set => character = value; }
}