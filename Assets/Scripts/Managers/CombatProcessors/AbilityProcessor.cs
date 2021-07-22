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
        Debug.Log(attacker + " attacked " + attackee + " with " + ability);
        ability.HandleAbility(attacker, attackee, ability);//format is always attacker, attackee

        //must check follow up after every ability
        followUpProcessor.HandleFollowUpAction(new FollowUpAction(attacker, attackee, ability));
    }
    /////////////////////////////////
    public void SplashDamage(Character attacker, Character attackee, float damage, float range, float radius){
        List<Character> charactersWithinRange = movementProcessor.GetCharactersInRange(attacker.transform.position, range, attackee.transform.position, radius);
        foreach(Character character1 in charactersWithinRange)
        {
            character1.DecrementHealth(damage);
        }
        //display circle around splash damage area temporarily
        //SplashDamageRange splashDamageRange = SplashDamageRange.FindObjectOfType<SplashDamageRange>();
        //splashDamageRange.Run(pos, range);
    }
    public void Damage(Character character, float damage){

        character.DecrementHealth(damage);
    }

    
    public void Heal(Character character, float heal){
        character.IncrementHealth(heal);

    }
    public void SplashHeal(Character attacker, Character attackee, float heal, float range, float radius)
    {
        List<Character> charactersWithinRange = new List<Character>();
        charactersWithinRange = movementProcessor.GetCharactersInRange(attacker.transform.position, range, attackee.transform.position, radius);
        foreach (Character character1 in charactersWithinRange)
        {
            character1.IncrementHealth(heal);
        }
    }

}
