using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UIHandler")]

public class UIHandler : ScriptableObject
{    
    AbilityButtonClicked onAbilityButtonClicked;
    FinishTurnButtonClicked onFinishTurnButtonClicked;

    LineRenderer validPathRenderer;
    LineRenderer invalidPathRenderer;
    private void OnEnable()
    {
        onAbilityButtonClicked = FindObjectOfType<AbilityButtonClicked>();//get these for all buttons/UI
        onAbilityButtonClicked.ManualAwake();
        onFinishTurnButtonClicked = FindObjectOfType<FinishTurnButtonClicked>();
        onFinishTurnButtonClicked.ManualAwake();
    }
    public void UpdateCombatTurnUI(Character character)
    {
        onAbilityButtonClicked.UpdateAbilities(character);
        onFinishTurnButtonClicked.UpdateButton(character.IsPlayer());
    }
    public void StopDisplayingCombat()
    {
        StopDisplayingAbilities();
        StopDisplayingEndTurn();
    }





    //TODO automatically turn off and on UI based off the manager being switched
    public void DisplayAbility()
    {

    }
    public void DisplayStatus()
    {

    }
    public void StopDisplayingEndTurn()
    {
        onFinishTurnButtonClicked.StopDisplay();
    }
    public void StopDisplayingAbilities()
    {
        onAbilityButtonClicked.StopDisplaying();
    }

}