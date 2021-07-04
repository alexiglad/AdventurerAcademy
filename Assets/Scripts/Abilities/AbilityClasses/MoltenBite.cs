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
#pragma warning disable
    private AbilityTypeEnum abilityType;
#pragma warning restore
    private void OnEnable()
    {
        damage = (FloatValueSO)CreateInstance("FloatValueSO");
        damage.SetFloatValue(4f);
        range = (FloatValueSO)CreateInstance("FloatValueSO");
        range.SetFloatValue(2f);
        abilityType = AbilityTypeEnum.Melee;
    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        abilityProcessor.SplashDamage(attackee, damage.GetFloatValue(), range.GetFloatValue());

    }
}
