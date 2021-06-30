using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatManager : GameStateManager
{
    #region Local Variables
    private SortedSet<Character> characters = new SortedSet<Character>();
    private SortedSet<Character> userCharacters = new SortedSet<Character>();
    private SortedSet<Character> enemyCharacters = new SortedSet<Character>();


    private IEnumerator<Character> enumerator;
    bool characterType;
    Turn turn;
    Character character;
    bool targeting;



    [SerializeField] AbilityProcessor abilityProcessorInstance;
    [SerializeField] StatusProcessor statusProcessorInstance;
    [SerializeField] MovementProcessor movementProcesssor;
    [SerializeField] UIHandler uiHandler;


    public Character Character { get => character; set => character = value; }
    public bool CharacterType { get => characterType; set => characterType = value; }
    public SortedSet<Character> Characters { get => characters; set => characters = value; }

    public Turn Turn { get => turn; set => turn = value; }
    public SortedSet<Character> UserCharacters { get => userCharacters; set => userCharacters = value; }


    #endregion
    public override void Start()
    {
        turn = new Turn();
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        character = enumerator.Current;
        targeting = false;

        ImportListeners();
        uiHandler = (UIHandler)FindObjectOfType(typeof(UIHandler));
        uiHandler.UpdateCombatTurnUI(character);

        

        abilityProcessorInstance = Resources.FindObjectsOfTypeAll<AbilityProcessor>()[0];
        statusProcessorInstance = Resources.FindObjectsOfTypeAll<StatusProcessor>()[0];
        movementProcesssor = Resources.FindObjectsOfTypeAll<MovementProcessor>()[0];
        //get player list
        
        foreach (Character characterE in characters)
        {
            if (characterE.IsPlayer())
                userCharacters.Add(characterE);
            else
            {
                enemyCharacters.Add(characterE);
            }
        }


    }

    public void UpdateIteration(Turn turnChange)
    {
        bool updated = UpdateTurn(turnChange);//this just adjusts the global variable turn appropriately
        if (ValidTurn(turnChange) && updated)
        {
            UpdateCharacters(turnChange);
        }
        

        if (TurnFinished())
            IterateCharacters();
    }
    public override void AddCharacters(SortedSet<Character> charactersPassed)
    {

        this.characters = charactersPassed;
    }

    public bool GetTargeting()
    {
        return targeting;
    }

    #region Custom Combat Manager Methods 

    public Turn DetermineEnemyTurn(Character character)//TODO
    {
        return character.EnemyAI.DetermineTurn(character);
    }
    public float GetRemainingMovement()
    {
        return this.character.GetMaxMovement() - this.turn.GetMovement().magnitude;
    }

    public bool UpdateTurn(Turn turnChange)
    {//TODO check if movement and ability are valid additions to the turn
        bool updated = false;
        if (turnChange.GetMovement() != Vector3.zero && turn.GetMovement().magnitude <= character.GetMaxMovement())
        {//add x and y components to turn only if the movement is less than the max movement
            turn.SetMovement(turnChange.GetMovement() + turn.GetMovement());
            updated = true;
        }
        if (turnChange.GetAbility() != null && turn.GetTarget() == null)//once they've invoked target with ability they cant do it again
        {//only update if current ability hasn't been handled on a target yet
            targeting = true;
            turn.SetAbility(turnChange.GetAbility());
            updated = true;
        }
        if (turnChange.GetTarget() != null && turn.GetTarget() == null)
        {//can't have already invoked an ability on a target yet
            turn.SetTarget(turnChange.GetTarget());
            updated = true;
        }
        return updated;
    }
    public void UpdateCharacters(Turn turnChange)
    {
        //call ability or move method if necessary
        if (turnChange.GetMovement() != Vector3.zero)//move turn
        {
            movementProcesssor.HandleMovement(character, turnChange.GetMovement());
        }
        if(turn.GetAbility() != null && turn.GetTarget() != null)//ability turn
        {
            abilityProcessorInstance.HandleAbility(character, turn.GetTarget(), turn.GetAbility());
        }
    }


    bool GetCharacterType()
    {
        return character.IsPlayer();
    }

    bool ValidTurn(Turn pushTurn)
    {
        if (pushTurn.GetMovement() != Vector3.zero)//TODO update all vector 2's to vector 3's
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
        else if (!character.IsPlayer())//temp code enemies automatically finish turn in one go
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
        Character tempCharacter = enumerator.Current;
        if(character == tempCharacter)//i.e. current character is dying get next character
        {
            if (enumerator.MoveNext())
            {
                tempCharacter = enumerator.Current;
            }
            else
            {
                tempCharacter = characters.Min;
            }
        }
        characters.Remove(character);
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        while (enumerator.Current != tempCharacter)//exit when tempCharacter pos is found
        {
            if (!enumerator.MoveNext())//failsafe code
            {
                enumerator.Reset();
                enumerator.MoveNext();
                Debug.Log("ERROR REMOVING CHARACTER");
                break;
            }
        }
        this.character = tempCharacter;
        //should effectively exit on correct position


        //decide if whole squad is dead
        if (!MoreThanOneSideIsAlive())
        {
            Debug.Log("Battle Ended!");
            EndBattle(character.IsPlayer());//if true is a win if false is a loss
        }
    }

    public void IterateCharacters()
    {
        if (MoreThanOneSideIsAlive())
        {
            turn = new Turn();
            if (enumerator.MoveNext())
            {
                character = enumerator.Current;
                characterType = GetCharacterType();
            }
            else
            {//TODO check if this works
                enumerator.Reset();
                enumerator.MoveNext();
                character = enumerator.Current;
                characterType = GetCharacterType();
            }

            uiHandler.UpdateCombatTurnUI(character);
            statusProcessorInstance.HandleStatuses(character);

            targeting = false;
            if (!characterType)
            {//only do this if is an enemy
                UpdateIteration(DetermineEnemyTurn(character));
            }
        }
        
    }
    bool MoreThanOneSideIsAlive()
    {
        int zero = 1;//used to check for f first iteration is done yet
        bool initial = false;
        foreach (Character character in characters)
        {
            if (zero == 1)
            {
                zero = 0;
                initial = character.IsPlayer();
            }
            else if (initial != character.IsPlayer())
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
    void ImportListeners()
    {

        FinishTurnButtonClicked onFinishTurnButtonClicked = FindObjectOfType<FinishTurnButtonClicked>();
        onFinishTurnButtonClicked.OnFinishTurnButtonClicked += FinishTurn;

        AbilityButtonClicked onAbilityButtonClicked = FindObjectOfType<AbilityButtonClicked>();
        onAbilityButtonClicked.OnAbilityButtonClicked += CombatAbility;

    }
    void CombatMove(object sender, EventArgs e)
    {
        Turn turnUpdate = new Turn(new Vector3(1, 1, 0));//TODO eventually replace with with generated vector based on mouse pos
        UpdateIteration(turnUpdate);
        Debug.Log("Theoretically movedCombat");
    }
    void CombatAbility(object sender, AbilityEventArgs e)
    {
        Turn turnUpdate = new Turn(e.NewAbility);
        UpdateIteration(turnUpdate);

    }
    public void CombatTarget(Character target)
    {
        Turn turnUpdate = new Turn(target);
        UpdateIteration(turnUpdate);

    }
    public void CombatMovement(Vector3 destination)
    {
        //NavMeshAgent agent = character.GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        //Debug.Log(agent.remainingDistance);
        if (character.Agent.CalculatePath(destination, path) && path.status == NavMeshPathStatus.PathComplete && destination.magnitude <= GetRemainingMovement())
        {
            Debug.Log("Agent Distance to travel " + destination.magnitude);
            Debug.Log("Player Remaining Movement " + GetRemainingMovement());
            character.Agent.SetDestination(destination);
        }              
    }
    void FinishTurn(object sender, EventArgs e)
    {
        IterateCharacters();
    }




    #endregion
}
