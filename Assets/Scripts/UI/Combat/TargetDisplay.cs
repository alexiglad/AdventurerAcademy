using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDisplay : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] MovementProcessor movementProcessor;
    [SerializeField] float startWidth;
    [SerializeField] float endWidth;

    RaycastData data;
    LineRenderer line;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = startWidth;
        line.endWidth = endWidth;
    }

    void Update()
    {
        line.positionCount = 0;
        data = controls.GetRaycastHit();

        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {

            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            if (tempRef.Character.IsPlayer() && data.HitBool && tempRef.GetTargeting() && controls.VerifyTag(data, "Character") && data.Hit.transform.GetComponent<Character>() != null)//user is targeting and selecting a character, make sure character us proper otherwise do not highlight character
            {
                HoverTarget(tempRef);
            }
            else if (data.HitBool && tempRef.GetTargeting())//user is targeting and is on terrain 
            {
                if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Melee ||
                    tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Ranged ||
                    tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Heal)
                {//display characters within range
                    DisplayWithinRange(tempRef);
                }
                else
                {//Display misc/splash
                    DisplayPointsWithinRange(tempRef);
                }
            }
            else
            {
                //display nothing
                ResetTargeting(tempRef);
            }
        }

    }
    public void DisplayWithinRange(CombatManager tempRef)
    {
        foreach (Character charactere in tempRef.Characters)
        {
            if(charactere == tempRef.Character)
            {
                charactere.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (movementProcessor.WithinRange(tempRef, charactere))
            {//character is valid to display
                if(tempRef.Character.IsPlayer() ^ charactere.IsPlayer())
                {
                    charactere.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    charactere.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
            else
            {//character is invalid to display
                charactere.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }
    public void DisplayPointsWithinRange(CombatManager tempRef)
    {
        //display pulsating ring around mouse position that locks onto characters, color changes based on validity
        //TODO when have shaders
    }

    public void HoverTarget(CombatManager tempRef)
    {
        //pulsate selected target quickly
        //TODO when have shaders
    }
    public void ResetTargeting(CombatManager tempRef)
    {
        foreach (Character charactere in tempRef.Characters)
        {
            charactere.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    
}

