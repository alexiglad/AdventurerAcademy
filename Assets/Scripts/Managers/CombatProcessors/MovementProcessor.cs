using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/MovementProcessor")]

public class MovementProcessor : ScriptableObject
{
    [SerializeField] FollowUpProcessor followUpProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public void HandleMovement(Character character, Vector3 movement)
    {
        //NavMeshAgent agent = character.GetComponent<NavMeshAgent>();
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        character.Agent.SetDestination(movement + characterBottom);
        if (character.IsPlayer())//TEMPORARY BUG FIX TODO
        {
            character.Animator.SetFloat("moveX", movement.x);
            character.Animator.SetFloat("moveZ", movement.z);
            character.Animator.SetBool("walking", true);
        }
        
        //Debug.Log(character + " traveled " + movement + " tiles with magnitude " + movement.magnitude + " at " + Vector3.Angle(new Vector3(1, 0, 0), movement) + " degrees");
        //TODO add followUpProcessor during navmesh path traversal!!!
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
    public bool WithinRange(CombatManager tempref, Character character2)
    {
        return Vector3.Distance(tempref.Character.transform.position, character2.transform.position) <= tempref.Turn.GetAbility().Range ;
    }
    public bool WithinRange(Character character, Character target, Ability ability)
    {
        return Vector3.Distance(character.transform.position, target.transform.position) <= ability.Range;
    }

}