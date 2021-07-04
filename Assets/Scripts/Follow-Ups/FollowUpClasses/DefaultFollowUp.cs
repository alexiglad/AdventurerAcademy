using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/DefaultFollowUp")]
public class DefaultFollowUp : FollowUp
{
    //this is the default follow up
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


        //this is where you bring your idea of what makes a follow-up trigger to life
        //in this case for simplicities sake im going to make it when a character uses zap and is within 2 unit

        //if(...)return true


        return false;
        //incorporate attacker and attackee into follow up event system
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        //this ability just hurts the 
        

    }
}