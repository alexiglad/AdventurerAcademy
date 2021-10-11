using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Splash damage attack that spreads fire on target
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/MoltenBite")]
public class AInfernoBreath : Ability
{


    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.SplashDamage(attackee, damage, radius);

    }
}
