using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Touch Of Death is an ability that does more damage
/// the lower the user's health is, as the user inflicts all their remaining might into the
/// attack
/// 
/// Thus, calculating the damage of the attack is based on the formula
/// 1-%user's health left 
/// 
/// Max damage will be 1/3 of the target character's health
/// 
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/TouchOfDeath")]
public class ATouchOfDeath : Ability
{
    [SerializeField] private float damageDivider;
    // Start is called before the first frame update
    void OnEnable()
    {
        damageDivider = 3;
        damage = 0;
        range = 10;
    }


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        float maxDamage = attackee.GetMaxHealth()/damageDivider;//max damage is 1/3 of health
        float damagePercent = 1 - attacker.GetPercentHealth();
        abilityProcessor.Damage(attackee, maxDamage * damagePercent);
        //attacker.DecrementHealth(attacker.GetMaxHealth());//kill user
    }
}
