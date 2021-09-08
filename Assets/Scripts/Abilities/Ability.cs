using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Ability : ScriptableObject
{  
    [SerializeField] protected AbilityProcessor abilityProcessor;
    [SerializeField] protected StatusProcessor statusProcessor;
    [SerializeField] protected GameStateManagerSO gameStateManager;


    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float radius;



    [SerializeField] AbilityTypeEnum abilityType;

    //UI
    [SerializeField] Sprite icon;
    [SerializeField] Sprite image;
    [SerializeField] AbilityImageTweenEnum direction;
    [SerializeField] float dimWidth;
    [SerializeField] float dimHeight;
    [SerializeField] float startX;
    [SerializeField] float startY;
    [SerializeField] float targetX;
    [SerializeField] float targetY;
    //UI

    public Sprite Icon { get => icon;}
    public AbilityTypeEnum AbilityType { get => abilityType; set => abilityType = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public Sprite Image { get => image;}
    public AbilityImageTweenEnum Direction { get => direction; }
    public float DimWidth { get => dimWidth;}
    public float DimHeight { get => dimHeight;}
    public float StartX { get => startX;}
    public float StartY { get => startY; }
    public float Radius { get => radius; set => radius = value; }
    public float TargetX { get => targetX; }
    public float TargetY { get => targetY; }

    private void Awake()
    {
        damage = 0;
        range = 0;
        radius = 0;
    }

    public virtual void HandleAbility(Character attacker, Character attackee, Ability ability) { }

}
