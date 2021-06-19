using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unimplemented
/// 
/// 
/// 
/// 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/TouchOfDeath")]
public class TouchOfDeath : Ability
{
    private FloatValueSO damage;
    // Start is called before the first frame update
    void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(5f);

    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        //unimplemented


    }
}
