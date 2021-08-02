using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/BirdSlam")]
///Birb picks up a knocked opponent (if an opponent is knocked) and flies 
///them into the air then body slams them
///
public class FUBirdSlam : FollowUp
{
    FloatValueSO damage;
    FloatValueSO range;
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(15f);
        range = (FloatValueSO)CreateInstance("FloatValueSO");
        range.SetFloatValue(1f);
    }


    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)


        if (followUpAction.FollowUpActionType == followUpType)
        {
            if (followUpAction.StatusEffect == StatusTypeEnum.Knocked && character.DifferentSides(followUpAction.Attackee))
                return true;
        }


        return false;
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        abilityProcessor.Damage(followUpAction.Attackee, damage.GetFloatValue());

        FollowUpFollowUp(followUpAction);
    }
}