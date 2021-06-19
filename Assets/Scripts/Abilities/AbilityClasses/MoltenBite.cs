using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MoltenBite is a low damage attack (2) that does 3 splash damage to every
/// character within a range of 5 (this is temporary lol)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/MoltenBite")]
public class MoltenBite : Ability
{
    private FloatValueSO damage;
    private FloatValueSO range;
    private void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(3f);
        range = (FloatValueSO)CreateInstance("FloatValueSO");
        range.SetFloatValue(5f);
    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessorInstance.SplashDamage(attackee, damage.GetFloatValue(), range.GetFloatValue());

    }
}
