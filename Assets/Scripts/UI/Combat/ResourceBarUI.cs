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

    [SerializeField] Character targetCharacter;
    Image bar;
    float change;
    bool important;

    [SerializeField] GameStateManagerSO gameStateManagerSO;


    private BarType BarType1 { get => barType;}
    public float CurrentValue { get => currentValue; set => currentValue = value; }
    public float MaxValue { get => maxValue; set => maxValue = value; }
    public Character TargetCharacter { get => targetCharacter; set => targetCharacter = value; }

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
            TargetCharacter = gameObject.GetComponentInParent<Character>();
        }
        if (TargetCharacter.IsPlayer() && barType == BarType.health)//had to choose only one as dont want both health bar parts adjusting the lean
        {
            TargetCharacter.OnCharacterDeath += HandleDeath;
        }
        if (gameStateManagerSO.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManagerSO.GetCurrentGameStateManager();
            tempRef.OnCharacterDamaged += UpdateValues;
        }
    }
    public void HandleDeath(object sender, CharacterDeathEventArgs data)
    {
        if(gameObject.GetComponentInParent<CanvasGroup>() != null)
        {
            gameObject.GetComponentInParent<CanvasGroup>().LeanAlpha(0, .7f);
            //TODO cedric right here implement thing adjusting canvases/portrait order (move all portraits below up)
        }

    }
    public void UpdateValues(object sender, CharacterDamagedArgs data)
    {
        if(TargetCharacter != null && TargetCharacter == data.character)
        {
            switch (barType)
            {
                case (BarType.health):
                    CurrentValue = TargetCharacter.GetHealth();
                    MaxValue = TargetCharacter.GetMaxHealth();
                    break;

                case (BarType.stamina):
                    CurrentValue = TargetCharacter.GetEnergy();
                    MaxValue = TargetCharacter.GetMaxEnergy();
                    break;

                case (BarType.healthBack):
                    CurrentValue = TargetCharacter.GetHealth();
                    MaxValue = TargetCharacter.GetMaxHealth();
                    break;
            }
            if (this.gameObject.activeInHierarchy)
                StartCoroutine(AnimateHealthBar());
        }          
    }

    IEnumerator AnimateHealthBar()
    {
        float targetSize = CurrentValue / MaxValue;
        if (barType == BarType.health)
        {
            change = targetSize - bar.fillAmount;
            if (bar.fillAmount > targetSize)
            {
                important = false;
                bar.fillAmount = targetSize;
            }
            if (bar.fillAmount < targetSize)
            {
                important = true;
                while (bar.fillAmount < targetSize)
                {
                    bar.fillAmount += (float)(Math.Pow(change, 0.7)  * .8 * Time.deltaTime);
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
                important = true;
                while (bar.fillAmount > targetSize)
                {
                    bar.fillAmount -= (float)(Math.Pow(change, 0.7)  * .8 * Time.deltaTime);
                    yield return null;
                }
                bar.fillAmount = targetSize;
            }
            if (bar.fillAmount < targetSize)
            {
                important = false;
                bar.fillAmount = targetSize;
            }
        }
        if (important)
        {
            targetCharacter.StopAllShaders();//TODO check if this works
            targetCharacter.TakingDamage = false;
            /*if (gameStateManagerSO.GetCurrentGameState() == GameStateEnum.Combat)
            {
                CombatManager tempRef = (CombatManager)gameStateManagerSO.GetCurrentGameStateManager();
                tempRef.EnableCombatInput();
            }*/
        }
    }

    public void SetCharacter(Character value)
    {
        TargetCharacter = value;
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
