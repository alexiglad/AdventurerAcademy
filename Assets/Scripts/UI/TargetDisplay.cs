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
            if (data.HitBool && tempRef.GetTargeting() && controls.VerifyTag(data, "Terrain"))//user is targeting and is on terrain 
            {
                if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Melee ||
                    tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Ranged)
                {//display enemy characters within range
                    DisplayEnemiesWithinRange(tempRef);
                }
                else if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Heal)
                {//display teammates within range
                    DisplayTeammatesWithinRange(tempRef);
                }
                else
                {//dont display people display selected point for misc/splash
                    DisplayPointsWithinRange(tempRef);
                }
            }
            else if(data.HitBool && tempRef.GetTargeting() && controls.VerifyTag(data, "Character"))//user is targeting and selecting a character, make sure character us proper otherwise do not highlight character
            {
                HoverTarget(tempRef);
            }
        }

    }
    public void DisplayEnemiesWithinRange(CombatManager tempRef)
    {
        foreach (Character charactere in tempRef.Characters)
        {
            if (movementProcessor.WithinRange(tempRef, charactere))
            {//character is valid to display
                //charactere.GetComponent<>
            }
            else
            {//character is invalid to display

            }
        }
    }
    public void DisplayTeammatesWithinRange(CombatManager tempRef)
    {
        foreach (Character charactere in tempRef.Characters)
        {
            if (movementProcessor.WithinRange(tempRef, charactere))
            {//character is valid to display

            }
            else
            {//character is invalid to display

            }
        }
    }
    public void DisplayPointsWithinRange(CombatManager tempRef)
    {
        //display pulsating ring that locks onto characters

    }

    public void HoverTarget(CombatManager tempRef)
    {
        //pulsate selected target quickly

    }
    
}

