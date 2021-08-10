using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/FollowUp/Peekaboo")]
///description
///
///
///
///
public class FUPeekaboo : FollowUp
{
    FloatValueSO damage;
    FloatValueSO range;
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(20f);
        range = (FloatValueSO)CreateInstance("FloatValueSO");
        range.SetFloatValue(6f);
    }


    public override bool IsValid(FollowUpAction followUpAction, Character character)//the character passes is from the followUp list
    {
        //must check in some sort for whether the attacker/attackee is the character who is using the followUp (as this makes sense)
        if (followUpAction.FollowUpActionType == followUpType)
        {
            if (character.Unstable && Vector3.Distance(character.transform.position, followUpAction.Attacker.transform.position) <= range.GetFloatValue())
            {
                character.Unstable = false;
                return true;
            }
        }


        return false;
    }
    public override void HandleFollowUp(FollowUpAction followUpAction)
    {
        abilityProcessor.Damage(followUpAction.Attacker, damage.GetFloatValue());
        FollowUpFollowUp(followUpAction);
    }
}