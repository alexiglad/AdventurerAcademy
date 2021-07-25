using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/UIHandler")]

public class UIHandler : ScriptableObject
{    
    AbilityButtonClicked onAbilityButtonClicked;
    FinishTurnButtonClicked onFinishTurnButtonClicked;
    AbilityImageDrawer abilityImageDrawer;
    TurnOrderScroll turnOrderScroll;
    Image doubleMovement;
    [SerializeField] protected GameStateManagerSO gameStateManager;

    public TurnOrderScroll TurnOrderScroll { get => turnOrderScroll; set => turnOrderScroll = value; }
    public FinishTurnButtonClicked OnFinishTurnButtonClicked { get => onFinishTurnButtonClicked; set => onFinishTurnButtonClicked = value; }

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
        doubleMovement = GameObject.Find("DoubleMovement").GetComponent<Image>();
        doubleMovement.enabled = false;
        turnOrderScroll.StartTurnOrder(tempRef.TurnOrder);
    }
    public void DisableCombat(List<Character> turnOrder)
    {
        StopDisplayingAbilities();
        StopDisplayingEndTurn();
        StopDisplayingTurnOrder(turnOrder);
        doubleMovement.enabled = false;

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
    public void DisplayDoubleMovement(bool doubleM)
    {
        if (doubleM)
        {
            doubleMovement.enabled = true;
        }
        else
        {
            doubleMovement.enabled = false;
        }
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
    public void DisplayEndTurn()
    {
        onFinishTurnButtonClicked.Display();
    }
    public void DisplayAbilities()
    {
        onAbilityButtonClicked.Display();
    }
    public void StopDisplayingTurnOrder(List<Character> turnOrder)
    {
        turnOrderScroll.StopDisplayingTurnOrder(turnOrder);
    }
    #endregion
}