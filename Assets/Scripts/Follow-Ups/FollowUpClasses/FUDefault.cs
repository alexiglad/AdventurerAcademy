using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/DefaultFollowUp")]
///description
///
///
///
///
public class FUDefault : FollowUp
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

        /*if (followUpAction.FollowUpActionType == followUpType)
        {
            if (condition)/* && followUpAction.Attackee == character
                return true;
        }*/


        return false;
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {


        FollowUpFollowUp(followUpAction);
    }
}