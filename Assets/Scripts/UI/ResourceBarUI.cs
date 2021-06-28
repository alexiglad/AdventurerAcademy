using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] bool isHealth;
    [SerializeField] float currentValue;
    [SerializeField] float maxValue;
    [SerializeField] Character character;

    Image bar;

    protected void Start()
    {
        bar = transform.GetComponent<Image>();
        if (character == null)
            character = gameObject.GetComponentInParent<Character>();
        if (isHealth) 
        {
            currentValue = character.GetHealth();
            maxValue = character.GetMaxHealth();
        }
        else
        {
            currentValue = character.GetEnergy();
            maxValue = character.GetMaxEnergy();
        }
    }

    protected void Update()
    {
        if(isHealth)
            currentValue = character.GetHealth();
        else
            currentValue = character.GetEnergy();

        SetSize(currentValue / maxValue); 
    }

    public void SetSize(float sizeNormalized)
    {
        bar.fillAmount = sizeNormalized;
    }
}
