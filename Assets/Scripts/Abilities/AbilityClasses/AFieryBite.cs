using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FieryBite inflicts a burn affect onto target as well as doing damage
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/FieryBite")]
public class AFieryBite : Ability
{


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.Damage(attackee, damage);
        int turns = Random.Range(3, 5);//in range of 3-4 (is max exclusive)
        Status status = new Status(damage, StatusTypeEnum.Burn, turns);
        statusProcessor.CreateStatus(attacker, attackee, status);

    }
}
