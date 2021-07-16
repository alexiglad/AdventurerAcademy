using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UIHandler")]

public class UIHandler : ScriptableObject
{    
    AbilityButtonClicked onAbilityButtonClicked;
    FinishTurnButtonClicked onFinishTurnButtonClicked;
    AbilityImageDrawer abilityImageDrawer;
    TurnOrderScroll turnOrderScroll;
    private void OnEnable()
    {
        onAbilityButtonClicked = FindObjectOfType<AbilityButtonClicked>();//get these for all buttons/UI
        onAbilityButtonClicked.ManualAwake();
        onFinishTurnButtonClicked = FindObjectOfType<FinishTurnButtonClicked>();
        onFinishTurnButtonClicked.ManualAwake();
        abilityImageDrawer = FindObjectOfType<AbilityImageDrawer>();
        turnOrderScroll = FindObjectOfType<TurnOrderScroll>();

    }

    #region combatUI
    public void UpdateCombatTurnUI(Character character)
    {
        onAbilityButtonClicked.UpdateAbilities(character);
        onFinishTurnButtonClicked.UpdateButton(character.IsPlayer());
    }
    public void StopDisplayingCombat()
    {
        StopDisplayingAbilities();
        StopDisplayingEndTurn();
        StopDisplayingTurnOrder();
    }

    public void DisplayAbility(Ability ability)
    {
        abilityImageDrawer.SetSprite(ability.Image);
        abilityImageDrawer.SetDirection(ability.Direction);
        abilityImageDrawer.SetDimensions(ability.DimWidth, ability.DimHeight);
        abilityImageDrawer.PlayAnimation();
    }
    public void DisplayStatus()
    {

    }
    public void UpdateTurnOrder(List<Character> turnOrder)
    {
        //TODO implement
    }
    public void StopDisplayingEndTurn()
    {
        onFinishTurnButtonClicked.StopDisplay();
    }
    public void StopDisplayingAbilities()
    {
        onAbilityButtonClicked.StopDisplaying();
    }
    public void StopDisplayingTurnOrder()
    {
        turnOrderScroll.StopDisplayingTurnOrder();
    }
    #endregion
}