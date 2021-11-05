using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StatusDrawer : MonoBehaviour
{
    public delegate void StatusAddDelegate(StatusData status, Sprite icon);
    public delegate void StatusRemoveDelegate(StatusData status);
    public event StatusAddDelegate OnStatusAdd;
    public event StatusRemoveDelegate OnStatusRemove;
    [SerializeField] protected GameStateManagerSO gameStateManager;
    [SerializeField] protected UIHandler uiHandler;
    [SerializeField] List<Sprite> statusIcons = new List<Sprite>();
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

    public void UpdateStatusUI(StatusData status)
    {
        //This code dosen't run properly Alexi, the else never runs
        /*if (status.Attackee.Statuses.Contains(status.Status) || !status.Attackee.IsPlayer())
        {
            return;
        }*/
        if (!status.Attackee.IsPlayer())
        {
            return;
        }
        else
        {            
            Sprite icon = null;
            foreach (Sprite sprite in statusIcons)
            {
                if (status.Status.StatusEffect.ToString() == sprite.name)
                {
                    icon = sprite;
                    break;
                }
            }
            OnStatusAdd?.Invoke(status, icon);
        }
    }
    public void DrawStatuses(StatusData status)
    {
        SpriteShaderTypeEnum shader = ConvertShader(status.Status.StatusEffect);
        status.Attackee.SetSpriteShader(shader);
        UpdateStatusUI(status);
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
