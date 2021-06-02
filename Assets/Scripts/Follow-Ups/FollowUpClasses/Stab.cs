using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : FollowUp
{
    //this ability is a "counter" to the zap ability
    //if an attacker zaps another player within 1 meter of that player that player will
    //automatically stab the attacker as a follow-up
    //base damage is 10
    //Zap zapInstance;
    FloatValueSO damage;
    void Awake()
    {
        //zapInstance = ZapSO.zapInstance;
        damage.SetFloatValue(10);
        Stab.CreateInstance("Stab");
    }


    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)


        //this is where you bring your idea of what makes a follow-up trigger to life
        //in this case for simplicities sake im going to make it when a character uses zap and is within 1 unit
        /*
        if (followUpAction.Ability == zapInstance && followUpAction.Attackee == character)
            return true;
        */
        return false;
        //incorporate attacker and attackee into follow up event system
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        //TODO implement actual ability stuff
        //this ability just hurts the 
        base.abilityProcessorInstance.DealDamage(followUpAction.Attacker, damage.GetFloatValue());  

    }
}