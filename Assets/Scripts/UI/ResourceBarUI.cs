using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] FloatValueSO currentValue;
    [SerializeField] FloatValueSO maxValue;
    Image bar;

    protected void Start()
    {
        bar = transform.GetComponent<Image>();
    }

    protected void Update()
    {
        SetSize(currentValue.GetFloatValue() / maxValue.GetFloatValue()); 
    }

    public void SetSize(float sizeNormalized)
    {
        bar.fillAmount = sizeNormalized;
        /*
        if (sizeNormalized>=0 && sizeNormalized<=1)
            bar.fillAmount = sizeNormalized;
        else
            switch (sizeNormalized<0)
            {
                case true: bar.fillAmount = 0; break;
                case false: bar.fillAmount = 1; break;
            }
        */
    }
}
