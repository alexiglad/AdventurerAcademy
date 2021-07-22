using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MoltenBite is a low damage attack (2) that does 3 splash damage to every
/// character within a range of 5 (this is temporary lol)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/MoltenBite")]
public class AMoltenBite : Ability
{
    private void OnEnable()
    {
        damage = 4;
        range = 4;
        radius = 1.5f;
    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.SplashDamage(attacker, attackee, damage, range, radius);

    }
}
