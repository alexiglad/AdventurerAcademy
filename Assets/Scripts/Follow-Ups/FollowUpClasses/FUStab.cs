using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/Stab")]
///this ability is a "counter" to the zap ability
///if an attacker zaps another player within 1 meter of that player that player will
///automatically stab the attacker as a follow-up
///attacker must be within 1 tile of attackee
///base damage is 10
public class FUStab : FollowUp
{
    
    FloatValueSO damage;
    FloatValueSO range;
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(10f);
        range = (FloatValueSO)CreateInstance("FloatValueSO");
        range.SetFloatValue(1f);
    }


    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)


        //this is where you bring your idea of what makes a follow-up trigger to life
        //in this case for simplicities sake im going to make it when a character uses zap and is within 2 unit
        if (followUpAction.FollowUpActionType == followUpType)
        {
            if (followUpAction.Ability.ToString().Equals("Zap (AZap)") &&
                followUpAction.Attackee == character &&
                Vector3.Distance(character.transform.position, followUpAction.Attacker.transform.position) <= 2)
                return true;
        }
        
        return false;
        //incorporate attacker and attackee into follow up event system
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        //this ability just hurts the 
        abilityProcessor.Damage(followUpAction.Attacker, damage.GetFloatValue());

        FollowUpFollowUp(followUpAction);
    }
}