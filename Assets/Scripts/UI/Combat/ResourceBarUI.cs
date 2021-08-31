using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] bool findCharacterInParent;

    [SerializeField] float currentValue;
    [SerializeField] float maxValue;
    [SerializeField] BarType barType;

    Character character;
    Image bar;
    float fillAmmount;
    float lerpTime = 3f;
    float timeElapsed = 0f;

    public float CurrentValue { get => currentValue;}
    public float MaxValue { get => maxValue;}
    private BarType BarType1 { get => barType;}

    private enum BarType
    {
        health,
        stamina,
        healthBack,
        staminaBack
    }
    
    void Start()
    {
        bar = transform.GetComponent<Image>();

        if (findCharacterInParent)
        {
            character = gameObject.GetComponentInParent<Character>();
            UpdateValues();
        }                   
    }

    public void UpdateValues()
    {
        switch (barType)
        {
            case (BarType.health):
                currentValue = character.GetHealth();
                maxValue = character.GetMaxHealth();
                break;

            case (BarType.stamina):
                currentValue = character.GetEnergy();
                maxValue = character.GetMaxEnergy();
                break;

            case (BarType.healthBack):
                currentValue = character.GetHealth();
                maxValue = character.GetMaxHealth();
                break;
        }
        fillAmmount = bar.fillAmount;
    }

    void Update()
    {
        UpdateValues();
        SetSize(currentValue / maxValue); 
    }

    public void SetSize(float sizeNormalized)
    {
        if (barType == BarType.health)
        {
            if (bar.fillAmount > sizeNormalized)
            {
                bar.fillAmount = sizeNormalized;
            }

            if (bar.fillAmount < sizeNormalized)
            {
                if (timeElapsed < lerpTime)
                {
                    bar.fillAmount = sizeNormalized / (this.timeElapsed / lerpTime);
                    timeElapsed += 1 * Time.deltaTime;
                    Debug.Log(this.timeElapsed);
                }
                else
                {
                    Debug.Log("ran 3");
                    bar.fillAmount = sizeNormalized;
                    this.timeElapsed = 0;
                }
            }            
        }

        if (barType == BarType.healthBack)
        {
            if (bar.fillAmount > sizeNormalized)
            {
                bar.color = Color.gray;
                if (this.timeElapsed < lerpTime)
                {
                    bar.fillAmount = sizeNormalized / (this.timeElapsed / lerpTime);
                    this.timeElapsed += 1 * Time.deltaTime;
                }
                else
                {
                    bar.fillAmount = sizeNormalized;
                    this.timeElapsed = 0;
                }
            }

            if (bar.fillAmount < sizeNormalized)
            {
                Debug.Log("ran back");
                bar.color = Color.green;
                bar.fillAmount = sizeNormalized;
            }
        }
    }

    public void SetCharacter(Character value)
    {
        character = value;
    }
}
