using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AbilityProcessor")]

public class AbilityProcessor : ScriptableObject
{

    /////////////////////////////////
    public void SplashDamage(Character character, float damage, float damageFallOff, float range){
        

        if(character.GetHealth().GetFloatValue()<=0){
            character.Dead();
        }
    }
    public void Damage(Character character, float damage){

        
        if(character.GetHealth().GetFloatValue() <= 0){
            character.Dead();
        }
    }

    
    public void Heal(Character character, float heal){

    }
    public void SplashHeal(Character character, float heal, float healFallOff, float range){

    }

}
