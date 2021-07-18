using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CombatManager : GameStateManager
{
    #region Local Variables
    private SortedSet<Character> characters = new SortedSet<Character>();
    private SortedSet<Character> userCharacters = new SortedSet<Character>();
    private SortedSet<Character> enemyCharacters = new SortedSet<Character>();
    private List<Character> turnOrder = new List<Character>();


    private IEnumerator<Character> enumerator;
    Turn turn;
    Character character;
    bool targeting;
    bool attacked;
    bool hasMovement;
    bool doubleMovement;
    bool canContinue;

    [SerializeField] GameController gameController;
    [SerializeField] AbilityProcessor abilityProcessorInstance;
    [SerializeField] StatusProcessor statusProcessorInstance;
    [SerializeField] MovementProcessor movementProcesssor;
    [SerializeField] UIHandler uiHandler;


    public Character Character { get => character; set => character = value; }
    public SortedSet<Character> Characters { get => characters; set => characters = value; }

    public Turn Turn { get => turn; set => turn = value; }
    public SortedSet<Character> UserCharacters { get => userCharacters; set => userCharacters = value; }
    public bool HasMovement { get => hasMovement; set => hasMovement = value; }
    public List<Character> TurnOrder { get => turnOrder; set => turnOrder = value; }
    public bool CanContinue { get => canContinue; set => canContinue = value; }


    #endregion
    public override void Start()
    {
        turn = new Turn();
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        character = enumerator.Current;
        character.GetComponent<SpriteRenderer>().color = Color.blue;
        //TODO add shader for character
        targeting = false;
        attacked = false;
        hasMovement = true;
        doubleMovement = false;
        canContinue = true;

        ImportListeners();
        uiHandler = (UIHandler)FindObjectOfType(typeof(UIHandler));
        uiHandler.UpdateCombatTurnUI(character);


        gameController = FindObjectOfType<GameController>();
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
        Debug.Log(character.name + " 's Turn!");

        foreach(Character characterE in characters)
        {
            turnOrder.Add(characterE);
        }
        if (!character.IsPlayer())
        {//only do this if is an enemy
            UpdateIteration(DetermineEnemyTurn(character), true);
        }
    }

    public void UpdateIteration(Turn turnChange, bool enemy)
    {
        if (enemy)
        {
            UpdateEnemyTurn(turnChange);
        }
        if (ValidTurn(turnChange))
        {
            UpdateCharacters(turnChange);
        }
        
        if (TurnFinished())
        {

            gameController.StartCoroutineCustom(IterateCharacters);
            /*StartCoroutine(test())
            WaitUntil(CanContinue());*/
            //IterateCharacters();

        }
    }
    public bool CanContinueMethod()
    {
        return canContinue;
    }
    public void UpdateEnemyTurn(Turn turnChange)
    {
        if (turnChange.GetMovement() != Vector3.zero && turn.AmountMoved <= character.GetMaxMovement() && hasMovement)
        {//add x and y components to turn only if the movement is less than the max movement
            turn.SetMovement(turnChange.GetMovement() + turn.GetMovement());//all turn.movement does is just store the total movement done on a given turn
            //turn.AmountMoved += Math.Abs(turnChange.GetMovement().magnitude - character.transform.position.magnitude);
            turn.AmountMoved += Vector3.Distance(character.transform.position, turnChange.GetMovement());
        }
        if (turnChange.GetAbility() != null && turn.GetTarget() == null)//once they've invoked target with ability they cant do it again
        {
            turn.SetAbility(turnChange.GetAbility());
        }
        if (turnChange.GetTarget() != null && turn.GetTarget() == null)
        {
            turn.SetTarget(turnChange.GetTarget());
        }
    }
    public bool UpdateAbility(Ability ability)
    {
        if (!attacked)
        {
            turn.SetAbility(ability);
            if(ability == null)
            {
                targeting = false;
            }
            else
            {
                targeting = true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool UpdateTarget(Character target)
    {
        if (!attacked && targeting)
        {
            turn.SetTarget(target);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool UpdateMovement(Vector3 movement)
    {
        if(turn.AmountMoved <= character.GetMaxMovement() && hasMovement)
        {
            turn.SetMovement(movement + turn.GetMovement());
            turn.AmountMoved += movement.magnitude;
            float error = .1f;
            if (GetRemainingMovement() <= error)
            {
                Debug.Log("User has used up movement for turn");
                hasMovement = false;
            }
            return true;
        }
        else
        {
            return false;
        }
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
        //Debug.Log("Character Max Movement: " + this.character.GetMaxMovement() + " - Ammount Moved this turn: " + this.turn.AmountMoved+ " = " + (this.character.GetMaxMovement() - this.turn.AmountMoved));
        return this.character.GetMaxMovement() - this.turn.AmountMoved;
    }

    public void UpdateCharacters(Turn turnChange)
    {
        //Debug.Log("turnChange " + turnChange.GetMovement());
        Ability currentAbility = turn.GetAbility();
        Vector3 currentMovement = turn.GetMovement();
        //call ability or move method if necessary
        if (currentMovement != Vector3.zero)//move turn
        {
            canContinue = false;
            movementProcesssor.HandleMovement(character, currentMovement);
        }
        if(currentAbility != null && currentAbility != null && !attacked)//ability turn
        {
            canContinue = false;
            uiHandler.DisplayAbility(currentAbility);
            if (turn.GetTarget().Inanimate)
            {
                HandleInanimateTarget(turnChange);
            }
            else
            {
                abilityProcessorInstance.HandleAbility(character, turn.GetTarget(), currentAbility);
            }
            targeting = false;
            attacked = true;
            uiHandler.StopDisplayingAbilities();
        }
    }
    public void HandleInanimateTarget(Turn turnChange)
    {
        if (turn.GetTarget().name == "voodoo")
        {
            Debug.Log("Voodoo ability used from " + character  + " onto " + turn.GetTarget().VoodooTarget);
            RemoveCharacter(turn.GetTarget());
            abilityProcessorInstance.HandleAbility(character, turn.GetTarget().VoodooTarget, turn.GetAbility());//TODO MAKE GET TARGET OF VOODOO
        }
    }

    bool ValidTurn(Turn pushTurn)
    {
        if (pushTurn.GetMovement() != Vector3.zero)
        {
            return true;
        }
        if (turn.GetAbility() != null && turn.GetTarget() != null)
        {
            return true;
        }       
        return false;        
    }

    bool TurnFinished()
    {
        if (!hasMovement && attacked)
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
    public void ResetEnumerator()
    {
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        while(enumerator.Current != character)
        {
            if (!enumerator.MoveNext())//failsafe code
            {
                enumerator.Reset();
                enumerator.MoveNext();
                Debug.Log("ERROR REMOVING CHARACTER");
                break;
            }
        }
    }
    public void RemoveCharacter(Character character)
    {
        turnOrder.Remove(character);
        uiHandler.UpdateTurnOrder(turnOrder);
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
        character.gameObject.SetActive(false);
        characters.Remove(character);
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();//FIX THIS HAVE TO ITERATE!!
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
            uiHandler.StopDisplayingCombat();
            EndBattle(character.IsPlayer());//if true is a win if false is a loss
        }
    }

    public void IterateCharacters()
    {
        if (turnOrder.Remove(character))
        {
            turnOrder.Add(character);
            uiHandler.UpdateTurnOrder(turnOrder);
        }
        if (MoreThanOneSideIsAlive())
        {
            character.GetComponent<SpriteRenderer>().color = Color.white;//todo fix with shaders

            turn = new Turn();
            if (enumerator.MoveNext())
            {
                character = enumerator.Current;
            }
            else
            {
                enumerator.Reset();
                enumerator.MoveNext();
                character = enumerator.Current;
            }
            Debug.Log(character.name + "'s Turn!");

            uiHandler.UpdateCombatTurnUI(character);
            statusProcessorInstance.HandleStatuses(character);
            targeting = false;
            attacked = false;
            hasMovement = true;
            doubleMovement = false;
            character.GetComponent<SpriteRenderer>().color = Color.blue;//TODO implement shaders
            if (character.Inanimate)
            {
                IterateCharacters();//verify this works
            }
            else if (!character.IsPlayer())
            {//only do this if is an enemy
                UpdateIteration(DetermineEnemyTurn(character), true);
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
    void CombatAbility(object sender, AbilityEventArgs e)
    {
        if (UpdateAbility(e.NewAbility))
        {
            Turn turnUpdate = new Turn(e.NewAbility);
            UpdateIteration(turnUpdate, false) ;
        }
    }
    public void CombatAbilityDeselect()
    {
        if (UpdateAbility(null))
        {
            AbilityEventArgs e = new AbilityEventArgs(null);
            Turn turnUpdate = new Turn(e.NewAbility);
            UpdateIteration(turnUpdate, false);
        }
        uiHandler.UnselectAbilities();
    }
    public void CombatTarget(Character target)
    {
        if (UpdateTarget(target))
        {
            Turn turnUpdate = new Turn(target);
            UpdateIteration(turnUpdate, false);
        }
    }
    public void CombatDoubleMove(bool value)
    {
        doubleMovement = true;
    }
    public void CombatMovement(Vector3 destination)
    {
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        Debug.Log("destination is" + destination);
        Debug.Log("character position is" + characterBottom);
        NavMeshPath path = new NavMeshPath();
        if (character.Agent.CalculatePath(destination, path) && path.status == NavMeshPathStatus.PathComplete) 
        {
            if (Vector3.Distance(destination, characterBottom) <= GetRemainingMovement())
            {
                UpdateIteration(new Turn(destination), false);
            }
            else
            {
                
                float distanceTraveled = 0;
                Vector3 location = new Vector3();
                Vector3 prev = characterBottom;
                //TODO fix nav mesh bug of always being off by 0.0633 on y axis
                foreach (Vector3 vector in path.corners)
                {
                    if (distanceTraveled + Vector3.Distance(vector, prev) >= GetRemainingMovement())
                    {
                        //if(GetRemainingMovement() - distanceTraveled    )
                        vector.Normalize();
                        Vector3 lastPath = (GetRemainingMovement() - distanceTraveled) * vector;
                        location += lastPath;
                        break;
                    }

                    else 
                    {
                        
                        distanceTraveled += Vector3.Distance(vector, prev);
                        location += vector;
                    }
                    prev = vector;
                }
                Debug.Log(location);
                NavMeshPath path2 = new NavMeshPath();

                if (character.Agent.CalculatePath(location, path2) && path2.status == NavMeshPathStatus.PathComplete)
                {
                    //this is creating the movement in the case of being outside the radius
                    UpdateIteration(new Turn(location), false);
                }
                else
                {
                    Debug.Log(path2.status == NavMeshPathStatus.PathComplete);
                    Debug.Log("error occured");
                }

            }
        }
    }

    public void CombatMovementTwo(Vector3 destination)
    {
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        Vector3 adjustedDestination = new Vector3(destination.x, destination.y , destination.z);
        NavMeshPath path = new NavMeshPath();
        if (character.Agent.CalculatePath(adjustedDestination, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            if (Vector3.Distance(adjustedDestination, characterBottom) <= GetRemainingMovement())
            {
                //If the destination is valid, move to destination
                UpdateMovement(adjustedDestination - characterBottom);
                UpdateIteration(new Turn(adjustedDestination - characterBottom), false);
            }
            else
            {
                //If the destination is not valid, find the closest point to the destination within range
                Vector3 newDestination = (adjustedDestination - characterBottom);
                newDestination.Normalize();
                newDestination = (characterBottom + (GetRemainingMovement()*newDestination));
                NavMeshPath newPath = new NavMeshPath();
                if(character.Agent.CalculatePath(newDestination, newPath) && newPath.status ==  NavMeshPathStatus.PathComplete)
                {
                    UpdateMovement(newDestination - characterBottom);
                    UpdateIteration(new Turn(newDestination - characterBottom), false);
                }
            }
        }

    }

    public bool IsInvalidPath(Vector3 destination)
    {
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        Vector3 adjustedDestination = new Vector3(destination.x, destination.y, destination.z);
        if (Vector3.Distance(adjustedDestination, characterBottom) <= GetRemainingMovement())
        {
            return false;
        }
        return true;
    }

    void FinishTurn(object sender, EventArgs e)
    {
        IterateCharacters();
    }

    #endregion
}
