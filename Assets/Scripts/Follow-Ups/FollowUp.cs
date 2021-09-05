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
    [SerializeField] float scaleX;
    [SerializeField] float scaleY;
    [SerializeField] float posX;
    [SerializeField] float posY;
    [SerializeField] float frameRate;
    [SerializeField] List<Sprite> followUpFrames;

    public FollowUpTypeEnum FollowUpType { get => followUpType; set => followUpType = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float ScaleX { get => scaleX;}
    public float ScaleY { get => scaleY;}
    public float PosX { get => posX;}
    public float PosY { get => posY;}
    public List<Sprite> Sprites { get => followUpFrames;}
    public float FrameRate { get => frameRate;}

    public float GetAnimationLength()
    {
        return frameRate * followUpFrames.Count;
    }

    public abstract bool IsValid(FollowUpAction followUpAction, Character character);
    public abstract void HandleFollowUp(FollowUpAction followUpAction);

    public void FollowUpFollowUp(FollowUpAction followUpAction)
    {
        followUpProcessor.HandleFollowUpAction(new FollowUpAction(followUpAction.Attacker, followUpAction.Attackee, this));
    }
}
