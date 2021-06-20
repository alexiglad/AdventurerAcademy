using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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


    private GameObject[] abilityButtons = new GameObject[5];
    private List<Ability> abilityButtonAbilities = new List<Ability>();



    void OnEnable()
    {
        abilityButtons[0] = GameObject.Find("AbilityOne");
        abilityButtons[1] = GameObject.Find("AbilityTwo");
        abilityButtons[2] = GameObject.Find("AbilityThree");
        abilityButtons[3] = GameObject.Find("AbilityFour");
        abilityButtons[4] = GameObject.Find("AbilityFive");


    }
    public void UpdateAbilities(Character character)
    {
        for (int i = 0; i<abilityButtons.Length; i++)
        {
            abilityButtons[i].SetActive(false);
        }
        
        if (character.GetPlayer())
        {
            abilityButtonAbilities = character.InUseAbilities;
            UpdateButtonUI();
            for (int i = 0; i < abilityButtonAbilities.Count; i++)
            {
                abilityButtons[i].SetActive(true);
            }
        }
    }
    void UpdateButtonUI()
    {//uses abilityButtonAbilities to correctly update button UI (I.e. text/images of buttons)
        //TODO
    }


    public void OnAblityButtonPressed(int pos)
    {
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs(abilityButtonAbilities[pos]));
        //temporarily debug ability clicked
        Debug.Log("Ability " + pos + "clicked");
    }

}
