using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] bool isHealth;
    [SerializeField] bool findCharacterInParent;
    [SerializeField] float currentValue;
    [SerializeField] float maxValue;
    
    Character character;
    Image bar;

    void Start()
    {
        bar = transform.GetComponent<Image>();

        if (findCharacterInParent)
        {
            character = gameObject.GetComponentInParent<Character>();
            UpdateValues();
        }                   
    }

    void UpdateValues()
    {
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

    void Update()
    {
        UpdateValues();
        SetSize(currentValue / maxValue); 
    }

    void SetSize(float sizeNormalized)
    {
        bar.fillAmount = sizeNormalized;
    }

    public void SetCharacter(Character value)
    {
        character = value;
    }
}
