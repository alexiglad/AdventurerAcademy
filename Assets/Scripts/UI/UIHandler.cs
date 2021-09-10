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
    TurnOrderScroll turnOrderScroll;
    GameObject doubleMovement;
    AbilityBarWidthAdjuster abilityBarWidthAdjuster;
    ResourceBarUI resourceBarUI;
    HoverHandler hoverHandler;
    [SerializeField] protected GameStateManagerSO gameStateManager;

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
        turnOrderScroll = FindObjectOfType<TurnOrderScroll>();
        hoverHandler = FindObjectOfType<HoverHandler>();
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        onFinishTurnButtonClicked.OnFinishTurnButtonClicked += tempRef.FinishTurn;
        abilityButton.OnAbilityButtonClicked += tempRef.CombatAbility;
        abilityButton.OnAbilityButtonHover += hoverHandler.DisplayAbilityHover;
        doubleMovement = GameObject.Find("DoubleMovement");
        doubleMovement.SetActive(false);
        turnOrderScroll.StartTurnOrder(tempRef.TurnOrder);
        abilityBarWidthAdjuster = FindObjectOfType<AbilityBarWidthAdjuster>();

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
        abilityImageDrawer.SetSprite(ability.Image);
        abilityImageDrawer.SetDirection(ability.Direction);
        abilityImageDrawer.SetStartingPosition(ability.StartX, ability.StartY);
        abilityImageDrawer.SetTargetPosition(ability.TargetX, ability.TargetY);
        abilityImageDrawer.PlayAnimation();        
    }
    public void DisplayFollowUp(FollowUpData followUp)
    {
        followUpAnimationDrawer.SetAnimationSprites(followUp.FollowUp.Sprites);
        followUpAnimationDrawer.SetScale(followUp.FollowUp.ScaleX, followUp.FollowUp.ScaleY);
        followUpAnimationDrawer.SetPosition(followUp.FollowUp.PosX, followUp.FollowUp.PosY);
        followUpAnimationDrawer.SetLength(followUp.FollowUp.GetAnimationLength());
        followUpAnimationDrawer.SetFrameRate(followUp.FollowUp.FrameRate);
        followUpAnimationDrawer.PlayAnimation();
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
        abilityButton.UnselectAbilities();
        abilityButton.Selected = false;
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