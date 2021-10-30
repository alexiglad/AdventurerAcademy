using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Handlers/UIHandler")]

public class UIHandler : ScriptableObject
{    
    AbilityButton abilityButton;
    FinishTurnButton onFinishTurnButtonClicked;
    AbilityImageDrawer abilityImageDrawer;
    FollowUpAnimationDrawer followUpAnimationDrawer;
    StatusDrawer statusDrawer;
    TurnOrderScroll turnOrderScroll;
    GameObject doubleMovement;
    AbilityBarWidthAdjuster abilityBarWidthAdjuster;
    ResourceBarUI resourceBarUI;
    HoverHandler hoverHandler;
    APBarHandler apBarHandler;
    [SerializeField] protected GameStateManagerSO gameStateManager;

    bool TEMP;

    public TurnOrderScroll TurnOrderScroll { get => turnOrderScroll; set => turnOrderScroll = value; }
    //public FinishTurnButton OnFinishTurnButtonClicked { get => onFinishTurnButtonClicked; set => onFinishTurnButtonClicked = value; }

    public void EnableCombat()
    {
        abilityButton = FindObjectOfType<AbilityButton>();//get these for all buttons/UI
        abilityButton.ManualAwake();
        onFinishTurnButtonClicked = FindObjectOfType<FinishTurnButton>();
        onFinishTurnButtonClicked.ManualAwake();
        abilityImageDrawer = FindObjectOfType<AbilityImageDrawer>();
        followUpAnimationDrawer = FindObjectOfType<FollowUpAnimationDrawer>();
        statusDrawer = FindObjectOfType<StatusDrawer>();
        turnOrderScroll = FindObjectOfType<TurnOrderScroll>();
        hoverHandler = FindObjectOfType<HoverHandler>();
        apBarHandler = FindObjectOfType<APBarHandler>();
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        onFinishTurnButtonClicked.OnFinishTurnButtonClicked += tempRef.FinishTurn;
        abilityButton.OnAbilityButtonClicked += tempRef.CombatAbility;
        abilityButton.OnAbilityButtonHover += hoverHandler.DisplayAbilityHover;
        doubleMovement = GameObject.Find("DoubleMovement");
        doubleMovement.SetActive(false);
        turnOrderScroll.StartTurnOrder(tempRef.TurnOrder);
        abilityBarWidthAdjuster = FindObjectOfType<AbilityBarWidthAdjuster>();
        TEMP = true;
    }
    public void DisableCombat(List<Character> turnOrder)
    {
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        StopDisplayingAbilities();
        StopDisplayingEndTurn();
        StopDisplayingTurnOrder(turnOrder);
        doubleMovement.SetActive(false);
        //Unsubscribe From Events
        onFinishTurnButtonClicked.OnFinishTurnButtonClicked -= tempRef.FinishTurn;
        abilityButton.OnAbilityButtonClicked -= tempRef.CombatAbility;
        abilityButton.OnAbilityButtonHover -= hoverHandler.DisplayAbilityHover;
    }

    #region combatUI
    public void UpdateCombatTurnUI(Character character)
    {
        abilityButton.UpdateAbilities(character);
        abilityBarWidthAdjuster.DrawAbilityBar();
        abilityButton.Selected = false;
        doubleMovement.SetActive(false);
        onFinishTurnButtonClicked.UpdateButton(character.IsPlayer());
    }

    public void DisplayAbility(Ability ability)
    {
        abilityImageDrawer.DisplayAbility(ability);  
    }
    public void DisplayFollowUp(FollowUpData followUp)
    {
        followUpAnimationDrawer.DisplayFollowUp(followUp);
    }
    public void DisplayStatus(StatusData status)
    {
        statusDrawer.DrawStatuses(status);
    }
    public void UnselectAbilities()
    {
        abilityButton.UnselectAbilities();
        abilityButton.Selected = false;
    }
    public void DisplayDoubleMovement(bool doubleM)
    {
        if (doubleM)
        {
            doubleMovement.SetActive(true);
            if (TEMP)
            {
                apBarHandler.PreviewAPCost(5);
                TEMP = false;
            }                
            else if (!TEMP)
            {
                apBarHandler.StopPreviewingAPCost();
                TEMP = true;
            }                
        }
        else
        {
            doubleMovement.SetActive(true);            
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
        abilityButton.StopDisplaying();
    }
    public void DisplayEndTurn()
    {
        onFinishTurnButtonClicked.Display();
    }
    public void DisplayAbilities()
    {
        abilityButton.Display();
    }
    public void StopDisplayingTurnOrder(List<Character> turnOrder)
    {
        turnOrderScroll.StopDisplayingTurnOrder(turnOrder);
    }
    #endregion
}