using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// 
/// 
/// 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/DefaultAbility")]
public class DefaultAbility : Ability
{
    private FloatValueSO damage;
    private new AbilityTypeEnum abilityType;
    // Start is called before the first frame update
    void OnEnable()
    {
        /*damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(DAMAGEf);
        abilityType = AbilityTypeEnum.TYPE*/
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {


    }
}
