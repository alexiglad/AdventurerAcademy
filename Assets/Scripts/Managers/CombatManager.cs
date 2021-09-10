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
    bool redoTurnOrder;
    bool resetted;
    bool initialStatus;
    bool battleEnded;

    Queue<Vector3> movementQueue;
    Queue<FollowUpData> followUpQueue;
    Queue<StatusData> statusQueue;
    List<DamageData> damagedCharacters;
    List<Character> deadCharacters;

    [SerializeField] GameStateManagerSO gameStateManager;
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
    public bool Attacked { get => attacked; set => attacked = value; }


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
        redoTurnOrder = false;
        resetted = false;
        initialStatus = false;
        battleEnded = false;

        movementQueue = new Queue<Vector3>();
        followUpQueue = new Queue<FollowUpData>();
        statusQueue = new Queue<StatusData>();
        damagedCharacters = new List<DamageData>();
        deadCharacters = new List<Character>();

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
            character.Obstacle.enabled = false;
            character.Agent.enabled = true;
        }
        else
        {
            IterateCharacters();
        }

        uiHandler.EnableCombat();
        uiHandler.UpdateCombatTurnUI(character);//todo check what happens when enemy starts combat

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
            gameStateManager.GetGameController().StartCoroutineCC(IterateCharacters);

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
            RemoveCharacter(turn.GetTarget());//TODO determine if this is necessary
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
        if (initialStatus)
        {
            initialStatus = false;//this is just for handling initial status bug
        }
        Character tempCharacter = enumerator.Current;
        bool moving = false;

        if (character == tempCharacter)//i.e. current character is dying get next character
        {
            resetted = true;
            gameStateManager.GetGameController().StartCoroutineCC(ResetTurn);
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
            if (character.Moving)//TODO change when adding follow up coroutines
            {
                character.Agent.isStopped = true;
                moving = true;
            }
        }
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

        if (turnOrder.Remove(character))//this happens after which is why its not working
        {
            deadCharacters.Add(character);
            if (TurnFinished())
            {
                if (moving)
                {
                    EnableCombatInput();
                }
            }
            else if (!TurnFinished() && !resetted)
            {
                if (!canContinue)
                {
                    redoTurnOrder = true;
                }
                else
                {
                    Debug.Log("CHECK CONDITION SHOULD NOT HAPPEN once cedric adds coroutines");
                    redoTurnOrder = true;
                    EnableCombatInput();
                }
            }
        }
        else
        {
            character.gameObject.SetActive(false);
            //todo change the turning of game objects off to right here
        }

        //decide if whole squad is dead
        if (!MoreThanOneSideIsAlive())
        {
            Debug.Log("Battle Over!");
            battleEnded = true;
        }
    }
    public void EnableCombatInput()
    {
        if (movementQueue.Any())
        {
            movementProcesssor.HandleMovement(character, movementQueue.Dequeue());
        }
        else if(followUpQueue.Any())
        {
            DisableCombatInput();
            uiHandler.DisplayFollowUp(followUpQueue.Dequeue());
        }
        else if(statusQueue.Any())
        {
            DisableCombatInput();
            uiHandler.DisplayStatus(statusQueue.Dequeue());
        }
        else if(damagedCharacters.Count > 0)
        {
            DisableCombatInput();
            uiHandler.DisplayDamage(damagedCharacters);
        }
        else if(deadCharacters.Count > 0)
        {
            DisableCombatInput();
            foreach (Character character in deadCharacters)
            {
                character.gameObject.SetActive(false);
                //todo add animation here to kill the character with coroutine
            }
            deadCharacters.Clear();
            EnableCombatInput();//TEMP CODE TODO
        }
        else if (battleEnded)
        {
            uiHandler.DisableCombat(turnOrder);
            EndBattle(DidUserWin());
        }
        else if (redoTurnOrder)
        {
            Action action = () => uiHandler.UpdateTurnOrder(turnOrder);
            gameStateManager.GetGameController().StartCoroutineTOS(0, action);
            redoTurnOrder = false;
        }
        else
        {
            canContinue = true;
            if (character.IsPlayer())
            {//do i need to disable/enable input too?, todo
                uiHandler.DisplayEndTurn();
            }
        }
    }
    public void DisableCombatInput()
    {
        canContinue = false;
        uiHandler.StopDisplayingEndTurn();
    }
    #region queues
    public void AddMovement(Vector3 vector)
    {
        movementQueue.Enqueue(vector);
    }
    public void AddFollowUp(FollowUpData followUp)
    {
        followUpQueue.Enqueue(followUp);
    }
    public void AddStatus(StatusData status)
    {
        statusQueue.Enqueue(status);
    }
    public void AddDamagedCharacter(DamageData damageData)
    {
        foreach(DamageData dd in damagedCharacters)
        {
            if(dd.Character == damageData.Character)
            {
                dd.HealthChange += damageData.HealthChange;
                return;
            }
        }
        damagedCharacters.Add(damageData);
    }
    #endregion
    public void IterateCharacters()
    {
        if (resetted)
        {
            resetted = false;
            return;
        }
        resetted = false;
        if (!character.Inanimate)
        {
            character.Agent.enabled = false;
            character.Obstacle.enabled = true;
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
            //statusProcessorInstance.HandleStatuses(character);//CHANGE THIS TO BE AFTER IN FINISH ITERATING
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
                character.Obstacle.enabled = false;
                if(changed)
                    gameStateManager.GetGameController().StartCoroutineNMA(FinishIterating, turnOrder);
            }
        }
    }
    public void FinishIterating()
    {
        character.Agent.enabled = true;
        initialStatus = true;
        statusProcessorInstance.HandleStatuses(character);//todo figure out a way to make this work with following line of code not killing people...
        if (!initialStatus)
        {
            //this means initialStatus was set to false just dont do other conditions
        }
        else if (!character.IsPlayer())
        {//only do this if is an enemy
            initialStatus = false;
            uiHandler.StopDisplayingAbilities();
            uiHandler.StopDisplayingEndTurn();
            UpdateIteration(DetermineEnemyTurn(character), true);
        }
        else
        {
            initialStatus = false;
            uiHandler.DisplayAbilities();
            uiHandler.DisplayEndTurn();
        }
    }
    public void ResetTurn()
    {
        turn = new Turn();
        targeting = false;
        attacked = false;
        hasMovement = true;
        doubleMovement = false;
        resetted = false;

        if (character.Inanimate)
        {
            IterateCharacters();//todo verify this works
        }
        else
        {
            character.Obstacle.enabled = false;
            gameStateManager.GetGameController().StartCoroutineNMA(FinishIterating, turnOrder);
        }
    }
    bool MoreThanOneSideIsAlive()
    {
        int zero = 1;//used to check for f first iteration is done yet
        bool initial = false;
        foreach (Character character in characters)
        {
            if (character.Inanimate)
            {
                continue;
            }
            else if (zero == 1)
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
    bool DidUserWin()
    {
        IEnumerator<Character> enumerator;
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        return enumerator.Current.IsPlayer();
    }
    #endregion

    #region Event Listeners

    void EndBattle(bool won)
    {
        if (won)
        {
            gameStateManager.GetGameLoader().LoadNextSubscene();
        }
        else
        {
            gameStateManager.GetGameLoader().LoadSceneAfterCombatLoss();
        }
    }
    public void CombatAbility(object sender, AbilityEventArgs e)
    {
        if (UpdateAbility(e.NewAbility) && e.Selected)
        {
            Turn turnUpdate = new Turn(e.NewAbility);
            UpdateIteration(turnUpdate, false) ;
        }
    }
    public void CombatAbilityDeselect()
    {
        if (UpdateAbility(null))
        {
            AbilityEventArgs e = new AbilityEventArgs(null, false);
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
