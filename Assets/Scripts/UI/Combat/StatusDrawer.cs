using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StatusDrawer : MonoBehaviour
{

    [SerializeField] protected GameStateManagerSO gameStateManager;
    [SerializeField] protected UIHandler uiHandler;
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
        SpriteShaderTypeEnum shader = ConvertShader(status.Status.StatusEffect);
        status.Attackee.SetSpriteShader(shader);
    }
    SpriteShaderTypeEnum ConvertShader(StatusTypeEnum status)
    {
        if(status == StatusTypeEnum.Regen)
        {
            return SpriteShaderTypeEnum.Regen;
        }
        else if (status == StatusTypeEnum.Burn)
        {
            return SpriteShaderTypeEnum.Burn;
        }
        else if (status == StatusTypeEnum.Poison)
        {
            return SpriteShaderTypeEnum.Poison;
        }
        else if (status == StatusTypeEnum.Frozen)
        {
            return SpriteShaderTypeEnum.Frozen;
        }
        else if (status == StatusTypeEnum.Sleep)
        {
            return SpriteShaderTypeEnum.Sleep;
        }
        else if (status == StatusTypeEnum.Knocked)
        {
            return SpriteShaderTypeEnum.Knocked;
        }
        else if (status == StatusTypeEnum.Blind)
        {
            return SpriteShaderTypeEnum.Blind;
        }
        else if (status == StatusTypeEnum.Drunk)
        {
            return SpriteShaderTypeEnum.Drunk;
        }
        else
        {
            Debug.Log("error");
            return SpriteShaderTypeEnum.None;
        }
    }
}
