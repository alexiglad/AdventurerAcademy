using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/FUBite ")]
///Wolves use this as a follow up to characters using melee attacks within 1 tile, 
///melee attack still go through but wolf bites attacker
///
///
///
///
public class FUBite : FollowUp
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