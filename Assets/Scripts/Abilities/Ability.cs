using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Ability : ScriptableObject
{

    public abstract void HandleAbility(Character attacker, Character attackee, Ability ability);

}
