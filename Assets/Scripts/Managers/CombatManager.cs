using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : GameStateManager
{
    #region Local Variables
    private SortedSet<Character> characters = new SortedSet<Character>();

    private IEnumerator<Character> enumerator;
    bool characterType;
    Turn turn;
    Character character;

    AbilityButtonClicked onAbilityButtonClicked;


    AbilityProcessor abilityProcessorInstance;
    StatusProcessor statusProcessorInstance;


    public Character Character { get => character; set => character = value; }
    public bool CharacterType { get => characterType; set => characterType = value; }
    public SortedSet<Character> Characters { get => characters; set => characters = value; }


    #endregion
    void Awake()
    {
        turn = new Turn();
        enumerator = characters.GetEnumerator();
        character = enumerator.Current;

        LeftClicked onLeftClicked = FindObjectOfType<LeftClicked>();
        onLeftClicked.OnLeftClicked += CombatMove;
        //TODO FIX THIS

        FinishTurnButtonClicked onFinishTurnButtonClicked = FindObjectOfType<FinishTurnButtonClicked>();
        onFinishTurnButtonClicked.OnFinishTurnButtonClicked += FinishTurn;


        onAbilityButtonClicked = FindObjectOfType<AbilityButtonClicked>();//reason we use array is there are multiple of these for each button
        onAbilityButtonClicked.OnAbilityButtonClicked += CombatAbility;

        TargetButtonClicked[] onTargetButtonClicked = FindObjectsOfType<TargetButtonClicked>();//reason we use array is there are multiple of these for each button
        foreach (TargetButtonClicked targetButton in onTargetButtonClicked)
        {
            targetButton.OnTargetButtonClicked += CombatTarget;
        }

        abilityProcessorInstance = (AbilityProcessor)FindObjectOfType(typeof(AbilityProcessor));
        statusProcessorInstance = (StatusProcessor)FindObjectOfType(typeof(StatusProcessor));


    }


    // Update is called once per frame

    public void UpdateIteration(Turn turnChange)
    {
        UpdateTurn(turnChange);//this just adjusts the global variable turn appropriately
        if (ValidTurn(turnChange))
        {
            UpdateCharacters(turnChange);
        }

        if (TurnFinished())
            IterateCharacters();
    }
    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
    }
    #region Custom Combat Manager Methods 

    public Turn DetermineEnemyTurn(Character character)//TODO
    {
        return character.EnemyAI.DetermineTurn(character);
        //return null;//implement event listeners
    }

    public void UpdateTurn(Turn turnChange)
    {
        if (turnChange.GetMovement() != Vector2.zero)
        {//add x and y components to turn
            turn.SetMovement(turnChange.GetMovement() + turn.GetMovement());
        }
        else if (turnChange.GetAbility() != null)
        {
            turn.SetAbility(turnChange.GetAbility());
        }
        else//has to be target
        {
            turn.SetTarget(turnChange.GetTarget());
        }
    }
    public void UpdateCharacters(Turn turnChange)
    {
        //call ability or move method if necessary
        if (turnChange.GetMovement() != Vector2.zero)//move turn
        {
            character.SetMovement(turnChange.GetMovement());
        }
        else//ability turn
        {
            character.InflictAbility(turn.GetTarget(), turn.GetAbility());
        }
    }


    bool GetCharacterType()
    {
        return character.GetPlayer();
    }

    bool ValidTurn(Turn pushTurn)
    {

        if (pushTurn.GetMovement() != null)
        {
            return true;
        }
        else if (turn.GetAbility() != null && turn.GetTarget() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool TurnFinished()
    {
        if (turn.GetMovement().magnitude >= character.GetMaxMovement() && turn.GetAbility() != null && turn.GetTarget() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);//iterate?TODO check if need to iterate
        //decide if whole squad is dead
        if (!MoreThanOneSideIsAlive())
        {
            EndBattle(character.GetPlayer());//if true is a win if false is a loss
        }
    }

    public void IterateCharacters()
    {
        turn = null;
        if (enumerator.MoveNext())
        {
            character = enumerator.Current;
            characterType = GetCharacterType();
        }
        else
        {//restart iteration through set however you do that
            character = enumerator.Current;
            characterType = GetCharacterType();
        }
        if (!characterType)
        {//only do this if is an enemy
            DetermineEnemyTurn(character);
        }
        onAbilityButtonClicked.UpdateAbilities(character);
        statusProcessorInstance.HandleStatuses(character);


    }
    bool MoreThanOneSideIsAlive()
    {
        int zero = 1;//used to check for f first iteration is done yet
        bool initial = false;
        //private IEnumerator<Character> tempEnum = characters.GetEnumerator();


        foreach (Character character in characters)
        {
            if (zero == 1)
            {
                zero = 0;
                initial = character.GetPlayer();
            }
            else if (initial != character.GetPlayer())
            {
                return true;
            }
        }
        return false;
    }
    void EndBattle(bool won)
    {
        FindObjectOfType<CombatOver>().TriggerEvent(won);
    }

    #endregion

    #region Event Listeners
    void CombatMove(object sender, EventArgs e)
    {
        Turn turnUpdate = new Turn(new Vector2(1, 1));//TODO eventually replace with with generated vector based on mouse pos
        UpdateIteration(turnUpdate);
        Debug.Log("Theoretically movedCombat");
    }
    void CombatAbility(object sender, AbilityEventArgs e)
    {
        Turn turnUpdate = new Turn(e.NewAbility);
        UpdateIteration(turnUpdate);

    }
    void CombatTarget(object sender, EventArgs e)
    {
        TargetButtonClicked sent = sender as TargetButtonClicked;
        Turn turnUpdate = new Turn(sent.Target);
        UpdateIteration(turnUpdate);

    }
    void FinishTurn(object sender, EventArgs e)
    {
        IterateCharacters();
    }




    #endregion
}
