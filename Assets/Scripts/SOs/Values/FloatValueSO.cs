using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Values/FloatValue")]
public class FloatValueSO : ScriptableObject
{
    [SerializeField] private float floatValue;
    public FloatValueSO(float value){
        floatValue= value;
    }
    public float GetFloatValue(){
        return floatValue;
    }

    public void SetFloatValue(float value)
    {
        floatValue = value;
    }
}
