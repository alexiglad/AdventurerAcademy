using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData
{
    Status status;
    Character attackee;

    public StatusData(Status status, Character attackee)
    {
        this.status = status;
        this.attackee = attackee;
    }

    public Status Status { get => status; set => status = value; }
    public Character Attackee { get => attackee; set => attackee = value; }
}