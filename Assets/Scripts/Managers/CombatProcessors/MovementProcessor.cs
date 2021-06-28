using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MovementProcessor")]

public class MovementProcessor : ScriptableObject
{
    float moveSpeed;
    [SerializeField] FollowUpProcessor followUpProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;

    public void HandleMovement(Character character, Vector3 movement)
    {
        while(movement != Vector3.zero)
        {
            UpdatePosition(character, movement);
            //need to add sleep timer here?
        }
        character.GetCharacterRigidBody().velocity = Vector3.zero;//stop after moving
        //disable controls temporarily
    }
    public void UpdatePosition(Character character, Vector3 movement)
    {
        Vector3 actualMovement;//this is basically the direction of the movement
        if (movement.magnitude > 1)
        {
            actualMovement = movement;
            actualMovement.Normalize();
        }
        else
        {
            actualMovement = movement;
        }
        //incorporate speed and other factors into actualMovement here 
        //update character position here
        actualMovement *= character.GetMoveSpeed();
        if(actualMovement.magnitude >= movement.magnitude)
        {
            actualMovement = movement;//can only move remaining distance
        }
        character.GetCharacterRigidBody().velocity = new Vector3(actualMovement.x , actualMovement.y, actualMovement.z);

        followUpProcessor.HandleFollowUpAction(new FollowUpAction(character, actualMovement));//this may be too much overhead to handle
    }
    public List<Character> GetCharactersInRange(Vector3 position, float range)
    {
        List<Character> charactersInRange = new List<Character>();
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        foreach (Character character in tempRef.Characters)
        {
            if(Vector3.Distance(character.transform.position, position) <= range) 
            {//this character is within range of ability/follow up
                charactersInRange.Add(character);
            }
        }
        return charactersInRange;
    }

}