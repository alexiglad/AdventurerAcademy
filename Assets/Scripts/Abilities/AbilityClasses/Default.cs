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
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Default")]
public class Default : Ability
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


    }
}
