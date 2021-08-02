using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ScriptableObjects/Managers/CombatManager")]
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
    bool charactersDied;

    GameController gameController;
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
        character.GetComponent<SpriteRenderer>().color = Color.white;//eventually add shaders
        //TODO add shader for character
        targeting = false;
        attacked = false;
        hasMovement = true;
        doubleMovement = false;
        canContinue = true;
        charactersDied = false;
        gameController = FindObjectOfType<GameController>();

        foreach (Character characterE in characters)
        {
            if (characterE.IsPlayer())
                userCharacters.Add(characterE);
            else
            {
                enemyCharacters.Add(characterE);
            }
        }
        foreach(Character characterE in characters)
        {
            turnOrder.Add(characterE);
        }
        if (!character.Inanimate)
        {
            character.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
            character.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
        else
        {
            IterateCharacters();
        }

        uiHandler.EnableCombat();
        uiHandler.UpdateCombatTurnUI(character);

        if (!character.IsPlayer())
        {//only do this if is an enemy
            UpdateIteration(DetermineEnemyTurn(character), true);
        }
    }

    public void UpdateIteration(Turn turnChange, bool turnFinished)
    {
        if (turnFinished)
        {
            UpdateEnemyTurn(turnChange);
        }
        if (ValidTurn(turnChange))
        {
            UpdateCharacters(turnChange);
        }
        
        if (turnFinished || TurnFinished())
        {
            gameController.StartCoroutineCC(IterateCharacters);

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
        if (!attacked && !doubleMovement)
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
        if (!attacked && targeting && !doubleMovement)
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
        if (doubleMovement && hasMovement && turn.AmountMoved + movement.magnitude <= 2* character.GetMaxMovement())
        {
            turn.SetMovement(movement + turn.GetMovement());
            turn.AmountMoved += movement.magnitude;
            float error = .2f;
            if (GetRemainingMovement() <= error)
            {
                Debug.Log("User has used up movement for turn");
                hasMovement = false;
            }
            return true;
        }
        else if(hasMovement && turn.AmountMoved + movement.magnitude <= character.GetMaxMovement() )
        {
            turn.SetMovement(movement + turn.GetMovement());
            turn.AmountMoved += movement.magnitude;
            float error = .2f;
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
    public bool UpdateMovement(Vector3 changeInLoc, float magnitude)//update this to take a destination and magnitude
    {
        if (doubleMovement && hasMovement && magnitude <= 2 * GetRemainingMovement())
        {
            turn.SetMovement(changeInLoc + turn.GetMovement());
            turn.AmountMoved += magnitude;
            float error = .2f;
            if (GetRemainingMovement() <= error)
            {
                Debug.Log("User has used up movement for turn");
                hasMovement = false;
            }
            return true;
        }
        else if (hasMovement && magnitude <= GetRemainingMovement())
        {
            turn.SetMovement(changeInLoc + turn.GetMovement());
            turn.AmountMoved += magnitude;
            float error = .2f;
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

    public override void SetSubstateEnum(SubstateEnum state)
    {
        this.State = state;
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
        return character.EnemyAI.DetermineTurn(character, this);
    }
    public float GetRemainingMovement()
    {
        if (doubleMovement)
        {
            return 2*this.character.GetMaxMovement() - this.turn.AmountMoved;
        }
        else
        {
            return this.character.GetMaxMovement() - this.turn.AmountMoved;
        }
    }

    public void UpdateCharacters(Turn turnChange)
    {
        Ability currentAbility = turn.GetAbility();
        Vector3 currentMovement = turnChange.GetMovement();
        //call ability or move method if necessary
        if (currentMovement != Vector3.zero)//move turn
        {
            DisableCombatInput();
            movementProcesssor.HandleMovement(character, currentMovement);
        }
        if(currentAbility != null && currentAbility != null && !attacked)//ability turn
        {
            DisableCombatInput();
            uiHandler.StopDisplayingAbilities();
            uiHandler.DisplayAbility(currentAbility);
            targeting = false;
            attacked = true;
            if (turn.GetTarget().Inanimate)
            {
                HandleInanimateTarget(turnChange);
            }
            else
            {
                abilityProcessorInstance.HandleAbility(character, turn.GetTarget(), currentAbility);
            }
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
        else if(turn.GetTarget().name == "TempCharacter(Clone)")
        {
            RemoveCharacter(turn.GetTarget());
            abilityProcessorInstance.HandleAbility(character, turn.GetTarget(), turn.GetAbility());//TODO MAKE GET TARGET OF VOODOO
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

        Character tempCharacter = enumerator.Current;
        bool reset = false;
        if (character == tempCharacter)//i.e. current character is dying get next character
        {
            Debug.Log("current character died");
            reset = true;
            if (enumerator.MoveNext())
            {
                tempCharacter = enumerator.Current;
            }
            else
            {
                enumerator.Reset();
                enumerator.MoveNext();
                tempCharacter = enumerator.Current;
            }
        }
        character.gameObject.SetActive(false);
        characters.Remove(character);
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        while (enumerator.Current != tempCharacter)
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

        if (turnOrder.Remove(character) && !TurnFinished() && MoreThanOneSideIsAlive())//dont double up
        {
            if (reset)
            {
                ResetTurn();
            }
            else if (!canContinue)
            {
                charactersDied = true;
            }
            else
            {
                Action action = () => uiHandler.UpdateTurnOrder(turnOrder);
                gameController.StartCoroutineTOS(0, action);
            }
        }
        //decide if whole squad is dead
        if (!MoreThanOneSideIsAlive())
        {
            Debug.Log("Battle Ended!");
            uiHandler.DisableCombat(turnOrder);
            EndBattle(character.IsPlayer());//if true is a win if false is a loss
        }
    }
    public void EnableCombatInput()
    {
        canContinue = true;
        uiHandler.DisplayEndTurn();
        //TODO add follow up animation queue here eventually
        if(charactersDied)
        {
            Action action = () => uiHandler.UpdateTurnOrder(turnOrder);
            gameController.StartCoroutineTOS(0, action);
            charactersDied = false;
        }
    }
    public void DisableCombatInput()
    {
        canContinue = false;
        uiHandler.StopDisplayingEndTurn();
    }
    public void IterateCharacters()
    {
        if (!character.Inanimate)
        {
            character.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            character.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
        }
        bool changed = false;
        if (turnOrder.Remove(character))
        {
            turnOrder.Add(character);
            changed = true;
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
            statusProcessorInstance.HandleStatuses(character);
            targeting = false;
            attacked = false;
            hasMovement = true;
            doubleMovement = false;
            
            if (character.Inanimate)
            {
                IterateCharacters();
            }
            else
            {
                character.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
                if(changed)
                    gameController.StartCoroutineNMA(FinishIterating, turnOrder);
            }
            
        }
    }
    public void FinishIterating()
    {
        character.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        if (!character.IsPlayer())
        {//only do this if is an enemy
            uiHandler.StopDisplayingAbilities();
            uiHandler.StopDisplayingEndTurn();
            UpdateIteration(DetermineEnemyTurn(character), true);
        }
        else
        {
            uiHandler.DisplayAbilities();
            uiHandler.DisplayEndTurn();
        }

    }
    public void ResetTurn()
    {
        turn = new Turn();
        statusProcessorInstance.HandleStatuses(character);
        targeting = false;
        attacked = false;
        hasMovement = true;
        doubleMovement = false;

        if (character.Inanimate)
        {
            IterateCharacters();//verify this works
        }
        else
        {
            character.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
            gameController.StartCoroutineNMA(FinishIterating, turnOrder);
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

    public void CombatAbility(object sender, AbilityEventArgs e)
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
    public void CombatDoubleMove()
    {
        if (doubleMovement)
        {
            if(turn.AmountMoved >= character.GetMaxMovement())
            {
                Debug.Log("cannot disable double movement you have already moved too much");
            }
            else
            {
                doubleMovement = false;
                uiHandler.DisplayDoubleMovement(doubleMovement);
                uiHandler.UpdateCombatTurnUI(character);
            }
        }
        else
        {
            if(!attacked && !targeting)
            {
                doubleMovement = true;
                hasMovement = true;
                uiHandler.DisplayDoubleMovement(doubleMovement);
                uiHandler.StopDisplayingAbilities();
            }
            else
            {
                if (attacked)
                {
                    Debug.Log("cannot enable double movement you have already attacked");
                }
                else
                {
                    Debug.Log("cannot enable double movement while targeting");
                }
            }
        }
    }

    public void CombatMovement(Vector3 destination)
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
                if (UpdateMovement(adjustedDestination - characterBottom))
                {
                    UpdateIteration(new Turn(adjustedDestination - characterBottom), false);
                }

            }
            else
            {
                float distanceTraveled = 0;
                Vector3 location = new Vector3();
                Vector3 prev = characterBottom;
                bool first = true;
                //prev.y += 0.08448386f;//TODO investigate why this helps eventually
                foreach (Vector3 vector in path.corners)
                {
                    if (distanceTraveled + Vector3.Distance(vector, prev) >= GetRemainingMovement())
                    {
                        Vector3 temp = vector - prev;
                        Vector3 lastPath = (GetRemainingMovement() - distanceTraveled) * (temp.normalized);
                        location += lastPath;
                        break;
                    }

                    else
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            distanceTraveled += Vector3.Distance(vector, prev);
                            location += vector - prev;
                        }
                    }
                    
                    prev = vector;
                }
                NavMeshPath path2 = new NavMeshPath();
                if (character.Agent.CalculatePath(location + characterBottom, path2) && path2.status == NavMeshPathStatus.PathComplete)
                {
                    //this is creating the movement in the case of being outside the radius
                    if (UpdateMovement(location, GetRemainingMovement()))
                    {
                        UpdateIteration(new Turn(location), false);
                    }
                    else
                    {
                        Debug.Log("another error occured");
                    }
                    //UpdateIteration(new Turn(location), false);
                }
                else
                {
                    Debug.Log("error occured");//TODO figure out why this happens
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

    public void FinishTurn(object sender, EventArgs e)
    {
        IterateCharacters();
    }

    #endregion
}
