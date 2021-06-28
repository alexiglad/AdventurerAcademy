using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UIHandler")]

public class UIHandler : ScriptableObject
{
    AbilityButtonClicked onAbilityButtonClicked;
    FinishTurnButtonClicked onFinishTurnButtonClicked;
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
        //TODO
        //update raycast for movement
        //update target characters selection
    }
    public void DisplayAbility()
    {

    }
    public void DisplayStatus()
    {

    }

}