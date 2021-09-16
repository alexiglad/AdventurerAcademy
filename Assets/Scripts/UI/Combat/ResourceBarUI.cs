using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] bool findCharacterInParent;

    [SerializeField] float currentValue = 1; //Default Value
    [SerializeField] float maxValue = 1; //Default Value
    [SerializeField] BarType barType;

    Character targetCharacter;
    Image bar;
    float lerpTime = 1f;
    float timeElapsed = 0f;
    float change;

    [SerializeField] GameStateManagerSO gameStateManagerSO;

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
            targetCharacter = gameObject.GetComponentInParent<Character>();
        }

        if (gameStateManagerSO.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManagerSO.GetCurrentGameStateManager();
            tempRef.OnCharacterDamaged += UpdateValues;
        }
    }

    public void UpdateValues(object sender, CharacterDamagedArgs data)
    {
        if(targetCharacter != null && targetCharacter == data.character)
        {
            switch (barType)
            {
                case (BarType.health):
                    currentValue = targetCharacter.GetHealth();
                    maxValue = targetCharacter.GetMaxHealth();
                    break;

                case (BarType.stamina):
                    currentValue = targetCharacter.GetEnergy();
                    maxValue = targetCharacter.GetMaxEnergy();
                    break;

                case (BarType.healthBack):
                    currentValue = targetCharacter.GetHealth();
                    maxValue = targetCharacter.GetMaxHealth();
                    break;
            }
            StartCoroutine(AnimateHealthBar());
        }          
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
                bar.color = Color.green;
                bar.fillAmount = sizeNormalized;
            }
        }
    }

    IEnumerator AnimateHealthBar()
    {
        float targetSize = currentValue / maxValue;
        if (barType == BarType.health)
        {
            change = targetSize - bar.fillAmount;
            if (bar.fillAmount > targetSize)
            {
                bar.fillAmount = targetSize;
            }
            if (bar.fillAmount < targetSize)
            {
                while (bar.fillAmount < targetSize)
                {
                    float amountLeft = targetSize - bar.fillAmount;
                    bar.fillAmount += (float)(Math.Pow(change, 0.7)  * Time.deltaTime);
                    yield return null;
                }
                bar.fillAmount = targetSize;
            }
        }
        else if (barType == BarType.healthBack)
        {
            change = bar.fillAmount - targetSize;
            if (bar.fillAmount > targetSize)
            {
                while (bar.fillAmount > targetSize)
                {
                    float amountLeft = bar.fillAmount - targetSize;

                    bar.fillAmount -= (float)(Math.Pow(change, 0.7)  * Time.deltaTime);
                    yield return null;
                }
                bar.fillAmount = targetSize;
            }
            if (bar.fillAmount < targetSize)
            {
                bar.fillAmount = targetSize;
            }
        }
    }

    public void SetCharacter(Character value)
    {
        targetCharacter = value;
    }

    private void OnDisable()
    {
        if(gameStateManagerSO.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManagerSO.GetCurrentGameStateManager();
            tempRef.OnCharacterDamaged -= UpdateValues;
        }
    }
}
