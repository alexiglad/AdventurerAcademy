using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    Vector2 movement;
    Ability ability;
    Character target;
    public Turn() { }
    public Turn(Vector2 movement)
    {
        this.movement = movement;
    }

    public Turn(Ability ability)
    {
        this.ability = ability;
    }

    public Turn(Character target)
    {
        this.target = target;
    }

    public bool IsEmpty()
    {
        return this.movement == Vector2.zero && this.ability == null && this.target == null;
    }

    public Vector2 GetMovement(){
        return movement;
    }
    public void SetMovement(Vector2 movement){
        this.movement = movement;
    }
    public Ability GetAbility(){
        return ability;
    }
    public void SetAbility(Ability ability){
        this.ability = ability;
    }
    public Character GetTarget(){
        return target;
    }
    public void SetTarget(Character target){
        this.target = target;
    }
}