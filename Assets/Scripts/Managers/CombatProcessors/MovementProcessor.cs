using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MovementProcessor")]

public class MovementProcessor : ScriptableObject
{
    float moveSpeed;
    FollowUpProcessor followUpProcessor;
    CombatManager combatManager;
    public void OnEnable()
    {
        followUpProcessor = (FollowUpProcessor)FindObjectOfType(typeof(FollowUpProcessor));
        combatManager = (CombatManager)FindObjectOfType(typeof(CombatManager));
    }
     
    public Vector2 UpdatePosition(Character character, Vector2 movement)
    {
        //TODO convert all to vector 3's
        Vector2 actualMovement = NormalizeMovement(movement);
        //incorporate speed and other factors into actualMovement here 
        moveSpeed = character.GetMoveSpeed();
        //update character position here
        actualMovement *= moveSpeed;
        character.GetCharacterRigidBody().velocity = new Vector2(actualMovement.x , actualMovement.y );

        followUpProcessor.HandleFollowUpAction(new FollowUpAction(character, actualMovement));
        return movement - actualMovement;
    }
    public Vector2 NormalizeMovement(Vector2 movement)
    {
        movement.Normalize();
        return movement;
    }
    public List<Character> GetCharactersInRange(Vector3 position, float range)
    {
        List<Character> charactersInRange = new List<Character>();
        foreach(Character character in combatManager.Characters)
        {
            if(Vector3.Distance(character.transform.position, position) <= range)
            {//this character is within range of ability/follow up
                charactersInRange.Add(character);
            }
        }
        return charactersInRange;
    }

}