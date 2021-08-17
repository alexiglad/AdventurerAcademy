using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Handlers/UIHandler")]

public class UIHandler : ScriptableObject
{    
    AbilityButtonClicked onAbilityButtonClicked;
    FinishTurnButtonClicked onFinishTurnButtonClicked;
    AbilityImageDrawer abilityImageDrawer;
    TurnOrderScroll turnOrderScroll;
    GameObject doubleMovement;
    AbilityBarWidthAdjuster abilityBarWidthAdjuster;
    ResourceBarUI resourceBarUI;
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
        doubleMovement = GameObject.Find("DoubleMovement");
        doubleMovement.SetActive(false);
        turnOrderScroll.StartTurnOrder(tempRef.TurnOrder);
        abilityBarWidthAdjuster = FindObjectOfType<AbilityBarWidthAdjuster>();

    }
    public void DisableCombat(List<Character> turnOrder)
    {
        StopDisplayingAbilities();
        StopDisplayingEndTurn();
        StopDisplayingTurnOrder(turnOrder);
        doubleMovement.SetActive(false);

    }

    #region combatUI
    public void UpdateCombatTurnUI(Character character)
    {
        onAbilityButtonClicked.UpdateAbilities(character);
        abilityBarWidthAdjuster.DrawAbilityBar();
        onFinishTurnButtonClicked.UpdateButton(character.IsPlayer());
    }


    public void DisplayAbility(Ability ability)
    {
        abilityImageDrawer.SetSprite(ability.Image);
        abilityImageDrawer.SetDirection(ability.Direction);
        abilityImageDrawer.SetStartingPosition(ability.StartX, ability.StartY);
        abilityImageDrawer.SetTargetPosition(ability.TargetX, ability.TargetY);
        abilityImageDrawer.PlayAnimation();        
    }
    public void DisplayFollowUp(FollowUpData followUp)
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            gameStateManager.GetGameController().StartCoroutineTime(1, tempRef.EnableCombatInput);
            //TODO this is temp
        }
    }
    public void DisplayStatus(StatusData status)
    {
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            gameStateManager.GetGameController().StartCoroutineTime(1, tempRef.EnableCombatInput);
            //TODO this is temp
        }
    }
    public void DisplayDamage(List<DamageData> damagedCharacters)
    {
        //when done execute two lines ot code below
        //damagedCharacters.Clear();//todo execute both lines of these code at end of animation
        //EnableCombatInput();//TEMP CODE TODO
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            gameStateManager.GetGameController().StartCoroutineTime(1, tempRef.EnableCombatInput);
            //TODO this is temp
            damagedCharacters.Clear();
        }
    }
    public void UnselectAbilities()
    {
        onAbilityButtonClicked.UnselectAbilities();
    }
    public void DisplayDoubleMovement(bool doubleM)
    {
        if (doubleM)
        {
            doubleMovement.SetActive(true);
        }
        else
        {
            doubleMovement.SetActive(false);
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