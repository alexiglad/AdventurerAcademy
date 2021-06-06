using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementProcessor : ScriptableObject
{
    float moveSpeed;
    FollowUpProcessor followUpProcessor;
    public void OnEnable()
    {
        followUpProcessor = (FollowUpProcessor)FindObjectOfType(typeof(FollowUpProcessor));
    }
     
    public Vector2 UpdatePosition(Character character, Vector2 movement)
    {
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
}