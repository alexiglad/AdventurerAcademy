using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/Processors/MovementProcessor")]

public class MovementProcessor : ScriptableObject
{
    [SerializeField] FollowUpProcessor followUpProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;
    readonly float threshold = 1;


    public void HandleMovement(Character character, Vector3 movement)
    {
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.DisableCombatInput();
        }
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        character.Agent.SetDestination(movement + characterBottom);

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
        character.Animator.SetBool("moving", true);
        if (!character.IsPlayer() || movement.magnitude >= threshold)
        {
            character.Animator.SetBool("running", true);
        }
        else
        {
            character.Animator.SetBool("walking", true);
        }

        //Debug.Log(character + " traveled " + movement + " tiles with magnitude " + movement.magnitude + " at " + Vector3.Angle(new Vector3(1, 0, 0), movement) + " degrees");
        Stabilize(character);
    }
    void Stabilize(Character character)
    {
        if (character.Unstable)
        {
            character.Unstable = false;
        }
    }
    public List<Character> GetCharactersInRange(Vector3 position2, float radius)
    {
        List<Character> charactersInRange = new List<Character>();
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            foreach (Character character in tempRef.Characters)
            {
                if (Vector3.Distance(character.transform.position, position2) <= radius)
                {//this character is within range of ability/follow up
                    charactersInRange.Add(character);
                }
            }
            return charactersInRange;
        }
        else
        {
            return null;
        }
    }
    public List<Character> GetCharactersInLine(Vector3 position, Vector3 position2, float radius)
    {
        List<Character> charactersInRange = new List<Character>();
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            foreach (Character character in tempRef.Characters)
            {
                Vector3 closestPoint = ClosestPointOnLine(position, position2, character.transform.position);
                if (Vector3.Distance(closestPoint, character.transform.position) <= radius)
                {
                    charactersInRange.Add(character);//TODO test
                }
            }
            return charactersInRange;
        }
        else
        {
            return null;
        }
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

    public Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return vA;

        if (t >= d)
            return vB;

        var vVector3 = vVector2 * t;

        var vClosestPoint = vA + vVector3;

        return vClosestPoint;
    }

}