using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FollowUp : ScriptableObject
{
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;
    [SerializeField] protected FollowUpProcessor followUpProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;

    [SerializeField] protected FollowUpTypeEnum followUpType;


    [SerializeField] protected float damage;
    [SerializeField] protected float range;


    /*image stuff here with sprite sheet*/
    [SerializeField] float dimWidth;
    [SerializeField] float dimHeight;
    [SerializeField] float startX;
    [SerializeField] float startY;
    [SerializeField] Sprite icon;
    [SerializeField] Sprite image;

    public FollowUpTypeEnum FollowUpType { get => followUpType; set => followUpType = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float DimWidth { get => dimWidth; set => dimWidth = value; }
    public float DimHeight { get => dimHeight; set => dimHeight = value; }
    public float StartX { get => startX; set => startX = value; }
    public float StartY { get => startY; set => startY = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public Sprite Image { get => image; set => image = value; }

    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
    public abstract void HandleFollowUp(FollowUpAction followUpAction);

    public void FollowUpFollowUp(FollowUpAction followUpAction)
    {
        followUpProcessor.HandleFollowUpAction(new FollowUpAction(followUpAction.Attacker, followUpAction.Attackee, this));
    }
}
