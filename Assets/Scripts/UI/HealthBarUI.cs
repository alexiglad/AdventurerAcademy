using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private FloatValueSO health;
    [SerializeField] private FloatValueSO maxHealth;
    private Transform bar;

    private void Start()
    {
        bar = transform.Find("Bar");
    }

    private void Update()
    {
        SetSize(health.GetFloatValue() / maxHealth.GetFloatValue());
    }

    public void SetSize(float sizeNormalized)
    {
        if(sizeNormalized>=0 && sizeNormalized<=1)
            bar.localScale = new Vector3(sizeNormalized, 1f);
        else
            switch (sizeNormalized<0)
            {
                case true: bar.localScale = new Vector3(0, 1f); break;
                case false: bar.localScale = new Vector3(1, 1f); break;
            }
    }
}
