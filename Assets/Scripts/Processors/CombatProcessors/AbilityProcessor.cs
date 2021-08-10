using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Processors/AbilityProcessor")]

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
    public void SplashDamage(Character attackee, float damage, float radius){
        List<Character> charactersWithinRange = movementProcessor.GetCharactersInRange(attackee.transform.position, radius);
        foreach(Character character1 in charactersWithinRange)
        {
            character1.DecrementHealth(damage);
        }
        //display circle around splash damage area temporarily
        //SplashDamageRange splashDamageRange = SplashDamageRange.FindObjectOfType<SplashDamageRange>();
        //splashDamageRange.Run(pos, range);
    }
    public void MovementDamage(Character attacker, Character attackee, float damage, float radius)
    {
        //needs to draw a line from attacker to attackee 
        //go through each character in combat and determine if they are range of closest point on line
        //if they are damage them
        List<Character> charactersWithinRange = movementProcessor.GetCharactersInLine(attacker.transform.position,attackee.transform.position, radius);
        foreach (Character character1 in charactersWithinRange)
        {
            if(character1 != attacker)
            {
                character1.DecrementHealth(damage);
            }
        }
        Vector3 characterBottom = attacker.BoxCollider.bounds.center;
        characterBottom.y -= attacker.BoxCollider.bounds.size.y / 2;
        movementProcessor.HandleMovement(attacker, attackee.transform.position - characterBottom);
    }
    public void BuildStats(Character character, Stats stats)
    {

        //TODO implement go through all stats and increase character's stats accordingly
    }
    public void Damage(Character character, float damage){

        character.DecrementHealth(damage);
    }
    
    public void Heal(Character character, float heal){
        character.IncrementHealth(heal);

    }
    public void SplashHeal(Character attackee, float heal, float radius)
    {
        List<Character> charactersWithinRange = movementProcessor.GetCharactersInRange(attackee.transform.position, radius);
        foreach (Character character1 in charactersWithinRange)
        {
            character1.IncrementHealth(heal);
        }
    }

}
