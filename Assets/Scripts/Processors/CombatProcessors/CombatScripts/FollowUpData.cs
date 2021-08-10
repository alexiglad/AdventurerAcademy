using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUpData
{
    FollowUp followUp;
    FollowUpAction followUpAction;

    public FollowUpData(FollowUp followUp, FollowUpAction followUpAction)
    {
        this.followUp = followUp;
        this.followUpAction = followUpAction;
    }

    public FollowUp FollowUp { get => followUp; set => followUp = value; }
    public FollowUpAction FollowUpAction { get => followUpAction; set => followUpAction = value; }
}