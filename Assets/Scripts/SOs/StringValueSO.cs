using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StringValue")]
public class StringValueSO : ScriptableObject
{
    [SerializeField] private string stringValue;
    public StringValueSO(string stringValue)
    {
        this.stringValue = stringValue;
    }
    public string GetStringValue()
    {
        return stringValue;
    }

    public void SetStringValue(string value)
    {
        stringValue = value;
    }
}
