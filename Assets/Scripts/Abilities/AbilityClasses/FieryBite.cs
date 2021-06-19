using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FieryBite is a basic monster ability that does a flat 3 damage. 
/// also inflicts a burn affect onto target with a turn range from 3-4
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/FieryBite")]
public class FieryBite : Ability
{
    private FloatValueSO damage;
    private void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(3f);
    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessorInstance.Damage(attackee, damage.GetFloatValue());
        int turns = Random.Range(3, 5);//in range of 3-4 (is max exclusive)
        Status status = new Status(3, StatusTypeEnum.Burn, turns);
        statusProcessorInstance.CreateStatus(attacker, attackee, status);

    }
}
