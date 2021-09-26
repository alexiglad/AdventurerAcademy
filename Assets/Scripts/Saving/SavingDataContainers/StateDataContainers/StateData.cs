using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateData
{
    CombatData combatData;
    RoamingData roamingData;
    public StateData(GameStateEnum gameState, GameStateManager gameStateManager)
    {
        if(gameState == GameStateEnum.Combat)
        {
            //todo
        }
        else if(gameState == GameStateEnum.Roaming)
        {

        }
    }

}
