using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityButtonClicked : MonoBehaviour
{
    //these are created programmatically for each button 
    public event EventHandler<AbilityEventArgs> OnAbilityButtonClicked;

    Ability ability1;
    Ability ability2;
    Ability ability3;
    Ability ability4;
    Ability ability5;
    private Button abilityButton1;
    private Button abilityButton2;
    private Button abilityButton3;
    private Button abilityButton4;
    private Button abilityButton5;


    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        abilityButton1 = rootVisualElement.Q<Button>("AbilityOne");
        abilityButton1.clicked += OnAbilityOnePressed;
        abilityButton2 = rootVisualElement.Q<Button>("AbilityTwo");
        abilityButton2.clicked += OnAbilityTwoPressed;
        abilityButton3 = rootVisualElement.Q<Button>("AbilityThree");
        abilityButton3.clicked += OnAbilityThreePressed;
        abilityButton4 = rootVisualElement.Q<Button>("AbilityFour");
        abilityButton4.clicked += OnAbilityFourPressed;
        abilityButton5 = rootVisualElement.Q<Button>("AbilityFive");
        abilityButton5.clicked += OnAbilityFivePressed;

    }
    public void UpdateAbilities(Character character)
    {
        List<Ability> inUseAbilities = character.InUseAbilities;
        if (character.GetPlayer()) {//if enemies doesnt matter
            ability1 = inUseAbilities[0];
            ability2 = inUseAbilities[1];
            ability3 = inUseAbilities[2];
            ability4 = inUseAbilities[3];
            ability5 = inUseAbilities[4];
            abilityButton1.SetEnabled(true);
            abilityButton2.SetEnabled(true);
            abilityButton3.SetEnabled(true);
            abilityButton4.SetEnabled(true);
            abilityButton5.SetEnabled(true);
        }
        else
        {
            abilityButton1.SetEnabled(false);
            abilityButton2.SetEnabled(false);
            abilityButton3.SetEnabled(false);
            abilityButton4.SetEnabled(false);
            abilityButton5.SetEnabled(false);
        }
    }

    void OnAbilityOnePressed()
    {
        Debug.Log("1pressed");
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs (ability1));
        
    }
    void OnAbilityTwoPressed()
    {
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs(ability2));
    }
    void OnAbilityThreePressed()
    {
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs(ability3));
    }
    void OnAbilityFourPressed()
    {
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs(ability4));
    }

    void OnAbilityFivePressed()
    {
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs(ability5));
    }

}
