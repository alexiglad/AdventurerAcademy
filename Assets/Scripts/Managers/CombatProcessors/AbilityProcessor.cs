using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProcessor : ScriptableObject
{

    /////////////////////////////////
    public void DealSplashDamage(Character character, float damage, float damageFallOff, float range){
        

        if(character.GetHealth().GetFloatValue()<=0){
            character.Dead();
        }
    }
    public void DealDamage(Character character, float damage){


        if(character.GetHealth().GetFloatValue() <= 0){
            character.Dead();
        }
    }
    public void DealDamageOvertime(Character character, float damage){


        if(character.GetHealth().GetFloatValue() <= 0){
            character.Dead();
        }
    }
    public void Heal(Character character, float heal){

    }
    public void HealSplash(Character character, float heal, float healFallOff, float range){

    }

}
