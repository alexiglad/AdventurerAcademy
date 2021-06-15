using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityStatuses
{
    None,//base case
    Regen,//heal
    Burn,//deal damage
    Poison,//
    Frozen,//
    Sleep,//skip turn
    Knocked,//have stand up animation and set turns left equal to 1 (so no movement)
    Drunk,//decreases accuracy and damage and makes movement a bit random
    Blind//can only do melee abilities decrease accuracy decrease movement
}
