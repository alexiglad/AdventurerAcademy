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
public class ADefault : Ability
{
    private FloatValueSO damage;
    void OnEnable()
    {
        /*damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(DAMAGEf);*/
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {


    }
}
