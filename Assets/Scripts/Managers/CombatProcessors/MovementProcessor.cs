using System.Collections;
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
        Vector3 actualMovement = movement - character.transform.position;
        character.Animator.SetFloat("moveX" , actualMovement.x);
        character.Animator.SetFloat("moveZ", actualMovement.z);
        character.Animator.SetBool("walking", true);
        Debug.Log(character + " traveled " + actualMovement + " tiles at " + Vector3.Angle(new Vector3(1, 0, 1), actualMovement) + " degrees");
        //TODO add followUpProcessor thingy
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