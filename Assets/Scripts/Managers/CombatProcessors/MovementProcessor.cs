﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/MovementProcessor")]

public class MovementProcessor : ScriptableObject
{
    float moveSpeed;
    [SerializeField] FollowUpProcessor followUpProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public void HandleMovement(Character character, Vector3 movement)
    {
        //NavMeshAgent agent = character.GetComponent<NavMeshAgent>();
        character.Agent.SetDestination(movement);
        Debug.Log(character + " traveled " + movement + " tiles at " + Vector3.Angle(Vector3.zero, movement) + " degrees");
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