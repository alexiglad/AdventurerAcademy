using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityButtonClicked : MonoBehaviour
{
    //these are created programmatically for each button 
    public event EventHandler<AbilityEventArgs> OnAbilityButtonClicked;



    private GameObject[] abilityButtons = new GameObject[5];
    private List<Ability> abilityButtonAbilities = new List<Ability>();//Todo Simplify?
    //Ceddy note: Kinda icky. Try to completely seperate the logic out of the ui using events.



    public void ManualAwake()
    {
        abilityButtons[0] = GameObject.Find("AbilityOne");
        abilityButtons[1] = GameObject.Find("AbilityTwo");
        abilityButtons[2] = GameObject.Find("AbilityThree");
        abilityButtons[3] = GameObject.Find("AbilityFour");
        abilityButtons[4] = GameObject.Find("AbilityFive");


    }
    public void StopDisplaying()
    {
        abilityButtons[0].transform.parent.gameObject.SetActive(false);
    }
    public void UpdateAbilities(Character character)
    {
        abilityButtons[0].transform.parent.gameObject.SetActive(true);
        for (int i = 0; i<abilityButtons.Length; i++)
        {
            abilityButtons[i].SetActive(false);
        }
        
        if (character.IsPlayer())
        {
            abilityButtonAbilities = character.GetCharacterData().GetInUseAbilities();
            for (int i = 0; i < abilityButtonAbilities.Count; i++)
            {
                abilityButtons[i].SetActive(true);
            }
        }
        else
        {
            StopDisplaying();
        }
    }
    
    public void UnselectAbilities()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            abilityButtons[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void OnAblityButtonPressed(int pos)
    {
        for(int i = 0; i<abilityButtons.Length; i++)
        {
            if(i == pos)
            {
                abilityButtons[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                abilityButtons[i].GetComponent<Image>().color = Color.white;
            }
        }
        OnAbilityButtonClicked?.Invoke(this, new AbilityEventArgs(abilityButtonAbilities[pos]));
    }

}
