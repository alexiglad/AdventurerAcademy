using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StatusDrawer : MonoBehaviour
{

    [SerializeField] protected GameStateManagerSO gameStateManager;
    private void Update()
    {
        if(gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            foreach (Character character in tempRef.Characters)
            {
                foreach(Status status in character.Statuses)
                {
                    //TODO implement this functionality cedric
                    //emit particle effects
                    if (character.IsPlayer())
                    {
                        //draw each status under character portrait
                    }
                }
            }

        }
    }
    public void DrawStatuses(StatusData status)
    {
        //here cedric implement "tick" effect
    }

}
