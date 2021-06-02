using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Zap : Ability
{
    private FloatValueSO damage;
    // Start is called before the first frame update
    void Awake()
    {
        //Gets Random Damage Between 1 and 10, rounded to the nearest int
        damage.SetFloatValue(3);
        Zap.CreateInstance("ZapSOInstance");
        //Zap zap = (Zap)Zap.FindObjectOfType(typeof(Zap));
        //this is the code to access an instance of zap (should always be the same instance)
    }
    

    //this is generally how we will format individual ability class
    //in Start set local variables and create instance
    
    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        //zap is an attack that simply damages the opponent, dealing no splash damage      

        //handle all ability stuff here, including the amount of damage to deal
        //in this case the zap attack (being basic) just deals a constant 3 damage
        base.abilityProcessorInstance.DealDamage(attackee, damage.GetFloatValue());
        //CombatManagerScriptableObject.combatManagerInstance.characters.Remove(this);		
    }
}
