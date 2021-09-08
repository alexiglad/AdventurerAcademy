using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/Bite ")]
///Wolves use this as a follow up to characters using melee attacks within 1 tile, 
///melee attack still go through but wolf bites attacker
///
public class FUBite : FollowUp
{

    void OnEnable()
    {
        damage = 5;
        range = 1;
    }


    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)
        if (followUpAction.FollowUpActionType == followUpType && character == followUpAction.Attackee)
        {
            if (Vector3.Distance(followUpAction.Attacker.transform.position, character.transform.position)<=range && followUpAction.Attackee ^ followUpAction.Attacker)
                return true;
        }


        return false;
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        abilityProcessor.Damage(followUpAction.Attacker, damage);

        FollowUpFollowUp(followUpAction);
    }
}