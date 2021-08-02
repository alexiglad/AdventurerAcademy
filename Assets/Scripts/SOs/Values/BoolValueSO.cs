using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Values/BooleanValue")]
public class BoolValueSO : ScriptableObject
{
    [SerializeField] private bool boolValue;
    public BoolValueSO(bool boolValue)
    {
        this.boolValue = boolValue;
    }
    public bool GetBoolValue()
    {
        return boolValue;
    }

    public void SetBoolValue(bool value)
    {
        boolValue = value;
    }
}
