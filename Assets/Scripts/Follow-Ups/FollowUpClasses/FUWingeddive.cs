﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/FUWingeddive")]
///Birb jumps in to protect teammates from abilities with his wings, absorbing most of 
///the damage of a ranged or melee attack
///Birb does this if within 1 tile of teammate
///
///
///
///
public class FUWingeddive : FollowUp
{
    FloatValueSO damage;
    FloatValueSO range;

    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(1f);
        range = (FloatValueSO)CreateInstance("FloatValueSO");
        range.SetFloatValue(1f);
    }


    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)

        //TODO implement

        /*if (followUpAction.FollowUpActionType == FollowUpActionTypeEnum.TYPE)
        {
            if (condition)
                return true;
        }*/


        return false;
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {


        FollowUpFollowUp(followUpAction);
    }
}