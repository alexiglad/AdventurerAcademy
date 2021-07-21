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
    [SerializeField] protected GameStateManagerSO gameStateManager;


    public void EnableCombat()
    {
        onAbilityButtonClicked = FindObjectOfType<AbilityButtonClicked>();//get these for all buttons/UI
        onAbilityButtonClicked.ManualAwake();
        onFinishTurnButtonClicked = FindObjectOfType<FinishTurnButtonClicked>();
        onFinishTurnButtonClicked.ManualAwake();
        abilityImageDrawer = FindObjectOfType<AbilityImageDrawer>();
        turnOrderScroll = FindObjectOfType<TurnOrderScroll>();
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        onFinishTurnButtonClicked.OnFinishTurnButtonClicked += tempRef.FinishTurn;
        onAbilityButtonClicked.OnAbilityButtonClicked += tempRef.CombatAbility;


    }
    public void DisableCombat()
    {
        StopDisplayingAbilities();
        StopDisplayingEndTurn();
        StopDisplayingTurnOrder();
    }

    #region combatUI
    public void UpdateCombatTurnUI(Character character)
    {
        onAbilityButtonClicked.UpdateAbilities(character);
        onFinishTurnButtonClicked.UpdateButton(character.IsPlayer());
    }


    public void DisplayAbility(Ability ability)
    {
        abilityImageDrawer.SetSprite(ability.Image);
        abilityImageDrawer.SetDirection(ability.Direction);
        //abilityImageDrawer.SetPosition(ability.StartX, ability.StartY);//TODO FIX THIS CEDRIC NEED TO ADD METHOD
        abilityImageDrawer.PlayAnimation();        
    }
    public void DisplayStatus()
    {

    }
    public void UnselectAbilities()
    {
        onAbilityButtonClicked.UnselectAbilities();
    }
    public void UpdateTurnOrder(List<Character> turnOrder)
    {
        turnOrderScroll.UpdateTurnOrder(turnOrder);
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