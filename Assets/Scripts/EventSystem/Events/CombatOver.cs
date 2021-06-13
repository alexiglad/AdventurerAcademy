using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatOver : MonoBehaviour
{
    public event EventHandler<BoolEventArgs> OnCombatOver;
    void Start()
    {

    }

    public void TriggerEvent(bool won)
    {
        OnCombatOver?.Invoke(this, new BoolEventArgs(won));
    }

}
