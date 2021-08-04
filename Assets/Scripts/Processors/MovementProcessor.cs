using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/Processors/MovementProcessor")]

public class MovementProcessor : ScriptableObject
{
    [SerializeField] FollowUpProcessor followUpProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public void HandleMovement(Character character, Vector3 movement)
    {
        
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        character.Agent.SetDestination(movement + characterBottom);
        if (character.IsPlayer())//TEMPORARY BUG FIX TODO
        {
            if(Mathf.Abs(movement.x) >= Mathf.Abs(movement.z))
            {
                if(movement.x >= 0)
                {//east
                    character.Direction = CardinaDirectionsEnum.East;
                }
                else
                {//west
                    character.Direction = CardinaDirectionsEnum.West;
                }
            }
            else
            {
                if (movement.z >= 0)
                {//north
                    character.Direction = CardinaDirectionsEnum.North;
                }
                else
                {//south
                    character.Direction = CardinaDirectionsEnum.South;
                }
            }
            character.Animator.SetFloat("moveX", movement.x);
            character.Animator.SetFloat("moveZ", movement.z);
            character.Animator.SetBool("walking", true);
        }
        
        Debug.Log(character + " traveled " + movement + " tiles with magnitude " + movement.magnitude + " at " + Vector3.Angle(new Vector3(1, 0, 0), movement) + " degrees");
    }
    public List<Character> GetCharactersInRange(Vector3 position, float range, Vector3 position2, float radius)
    {
        List<Character> charactersInRange = new List<Character>();
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        foreach (Character character in tempRef.Characters)
        {
            if(Vector3.Distance(position2, position) <= range && (Vector3.Distance(character.transform.position, position2) <= radius))
            {//this character is within range of ability/follow up
                charactersInRange.Add(character);
            }
        }
        return charactersInRange;
    }
    public bool WithinRange(CombatManager tempref, Character character2)
    {
        return Vector3.Distance(tempref.Character.transform.position, character2.transform.position) <= tempref.Turn.GetAbility().Range + tempref.Turn.GetAbility().Radius;
    }
    public bool WithinRange(CombatManager tempref, Character character, Vector3 pos)
    {
        return Vector3.Distance(character.transform.position, pos) <= tempref.Turn.GetAbility().Radius &&
            Vector3.Distance(tempref.Character.transform.position, pos) <= tempref.Turn.GetAbility().Range;
    }
    public bool WithinSplashRange(CombatManager tempref, Vector3 pos)
    {
        return Vector3.Distance(tempref.Character.transform.position, pos) <= tempref.Turn.GetAbility().Range;
    }

}