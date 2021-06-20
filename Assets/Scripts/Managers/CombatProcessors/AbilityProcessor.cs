using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AbilityProcessor")]

public class AbilityProcessor : ScriptableObject
{
    [SerializeField] MovementProcessor movementProcessor;
    [SerializeField] FollowUpProcessor followUpProcessor;

    public void HandleAbility(Character attacker, Character attackee, Ability ability)
    {

        ability.HandleAbility(attacker, attackee, ability);//format is always attacker, attackee
        Debug.Log(attacker + " attacked " + attackee + " with " + ability);
        //must check follow up after every ability
        Debug.Log("PROCESSOR IS " + movementProcessor);
        followUpProcessor.HandleFollowUpAction(new FollowUpAction(attacker, attackee, ability));
    }
    /////////////////////////////////
    public void SplashDamage(Character character, float damage, float range){
        List<Character> charactersWithinRange = new List<Character>();
        charactersWithinRange = movementProcessor.GetCharactersInRange(character.transform.position, range);
        foreach(Character character1 in charactersWithinRange)
        {
            character1.DecrementHealth(damage);
        }
    }
    public void Damage(Character character, float damage){

        character.DecrementHealth(damage);
    }

    
    public void Heal(Character character, float heal){
        character.IncrementHealth(heal);

    }
    public void SplashHeal(Character character, float heal, float range){
        List<Character> charactersWithinRange = new List<Character>();
        charactersWithinRange = movementProcessor.GetCharactersInRange(character.transform.position, range);
        foreach (Character character1 in charactersWithinRange)
        {
            character1.IncrementHealth(heal);
        }
    }

}
