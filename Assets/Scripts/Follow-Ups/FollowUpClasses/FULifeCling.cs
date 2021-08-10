using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/LifeCling")]
///when witch is going to die uses life cling which heals her back to half health
///
public class FULifeCling : FollowUp
{

    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)


        if (followUpAction.FollowUpActionType == followUpType && character == followUpAction.Attackee)
            return true;


        return false;
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        followUpAction.Attackee.SetHealth(followUpAction.Attackee.GetMaxHealth() / 2);

        FollowUpFollowUp(followUpAction);
    }
}