using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Managers/CombatManager")]
public class CombatManager : GameStateManager
{
    #region Local Variables
    private SortedSet<Character> characters = new SortedSet<Character>();
    private SortedSet<Character> userCharacters = new SortedSet<Character>();
    private SortedSet<Character> enemyCharacters = new SortedSet<Character>();
    private List<Character> turnOrder = new List<Character>();
    public event EventHandler<CharacterDamagedArgs> OnCharacterDamaged;

    private IEnumerator<Character> enumerator;
    Character character;

    Ability currentAbility;//user for storage of current ability

    bool attackingOrMoving;//true is attacking false is moving, defaults to true
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
    public SortedSet<Character> UserCharacters { get => userCharacters; set => userCharacters = value; }
    public List<Character> TurnOrder { get => turnOrder; set => turnOrder = value; }
    public bool CanContinue { get => canContinue; set => canContinue = value; }
    public Ability CurrentAbility { get => currentAbility; set => currentAbility = value; }
    public bool AttackingOrMoving { get => attackingOrMoving; set => attackingOrMoving = value; }


    #endregion
    public override void Start()
    {
        enumerator = characters.GetEnumerator();
        enumerator.MoveNext();
        character = enumerator.Current;
        character.GetComponent<SpriteRenderer>().color = Color.white;//eventually add shaders
        //TODO add shader for character
        attackingOrMoving = true;
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
            DetermineEnemyTurn(character);
        }
    }

    public void EndTurn()
    {
        gameStateManager.GetGameController().StartCoroutineCC(IterateCharacters);
    }

    public bool CanContinueMethod()
    {
        return canContinue;
    }

    public void UpdateAbility(Ability ability)
    {
        this.currentAbility = ability;
    }
    public bool GetTargeting()
    {
        return this.currentAbility != null;
    }
    public void UpdateTarget(Character target)
    {
        DisableCombatInput();
        uiHandler.UnselectAbilities();
        uiHandler.DisplayAbility(currentAbility);
        character.DecrementActionPoints(currentAbility.ActionPointCost);

        if (target.Inanimate)
        {
            HandleInanimateTarget(target);
        }
        else
        {
            abilityProcessorInstance.HandleAbility(character, target, currentAbility);
        }
        currentAbility = null;
    }
    public void UpdateMovement(Vector3 movement, float pathLength)
    {
        DisableCombatInput();
        int cost = (int)Math.Ceiling(pathLength * character.DistancePerActionPoint);
        character.DecrementActionPoints(cost);
        movementProcesssor.HandleMovement(character, movement);
    }

    public override void SetSubstateEnum(SubstateEnum state)
    {
        this.State = state;
    }

    public override void AddCharacters(SortedSet<Character> charactersPassed)
    {

        this.characters = charactersPassed;
    }


    #region Custom Combat Manager Methods 

    public void DetermineEnemyTurn(Character character)//TODO
    {
        Turn turn = character.EnemyAI.DetermineTurn(character, this);
        if(turn.GetMovement() != Vector3.zero)
            UpdateMovement(turn.GetMovement(), 2);
        else if(turn.GetAbility() != null)
        {//assumes if it has an ability it has a target
            UpdateAbility(turn.GetAbility());
            UpdateTarget(turn.GetTarget());
        }
        EndTurn();
    }

    public void HandleInanimateTarget(Character target)
    {
        if (target.name == "voodoo")
        {
            Debug.Log("Voodoo ability used from " + character  + " onto " + target.VoodooTarget);
            RemoveCharacter(target);
            abilityProcessorInstance.HandleAbility(character, target.VoodooTarget, currentAbility);//TODO MAKE GET TARGET OF VOODOO
        }
        else if(target.name == "TempCharacter(Clone)")
        {
            RemoveCharacter(target);//TODO determine if this is necessary
            abilityProcessorInstance.HandleAbility(character, target, currentAbility);//TODO MAKE GET TARGET OF VOODOO
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
                Debug.Log("here");
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
            if (character.IsPlayer())
            {
                if (moving)
                {
                    EnableCombatInput();
                }
            }
            else if (!resetted)
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
            foreach(DamageData data in damagedCharacters)
            {
                if(data.HealthChange == 0)
                {
                    continue;
                }
                else if(data.HealthChange > 0)
                {
                    data.Character.SetSpriteShader(SpriteShaderTypeEnum.Heal);
                }
                else
                {
                    data.Character.SetSpriteShader(SpriteShaderTypeEnum.Damage);
                }
                OnCharacterDamaged?.Invoke(this, new CharacterDamagedArgs(data.Character));
            }
            damagedCharacters.Clear();
            EnableCombatInput();
        }
        else if(deadCharacters.Count > 0)
        {
            DisableCombatInput();
            foreach (Character character in deadCharacters)
            {
                Action action0 = () => character.gameObject.LeanAlpha(0, .7f);// Character
                action0 += () => character.gameObject.GetComponent<CanvasGroup>().LeanAlpha(0, .7f);// Healthbar
                //action0 += character.setShader(); TODO implement cedric
                Action action1 = () => character.gameObject.SetActive(false);
                action1 += () => EnableCombatInput();
                Action gameContollerAction = () => gameStateManager.GetGameController().StartDoubleCoroutineTime(1, action0, action1);
                Action characterAction = () => character.SetSpriteShader(SpriteShaderTypeEnum.Death);
                character.AwaitTakingDamage(gameContollerAction, characterAction);
                //gameStateManager.GetGameController().StartDoubleCoroutineTime(1, action0, action1);

            }
            deadCharacters.Clear();
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
        if(damageData.HealthChange == 0)
        {
            return;
        }
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
            //@HERE SET ANIMATION FOR CURRENT SPRITE AND USE CINEMACHINE
            currentAbility = null;
            attackingOrMoving = true;
            
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
        character.IncrementActionPoints(character.ActionPointsPerTurn);
        initialStatus = true;
        statusProcessorInstance.HandleStatuses(character);//todo figure out a way to make this work with following line of code not killing people...
        EnableCombatInput();
        //TODO save after every turn here
        if (!initialStatus)
        {
            //this means initialStatus was set to false just dont do other conditions
        }
        else if (!character.IsPlayer())
        {//only do this if is an enemy
            initialStatus = false;
            uiHandler.StopDisplayingAbilities();
            uiHandler.StopDisplayingEndTurn();
            DetermineEnemyTurn(character);
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
        //@HERE SET ANIMATION FOR CURRENT SPRITE AND USE CINEMACHINE
        currentAbility = null;
        attackingOrMoving = true;
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
    public bool HasSufficientAP(Ability ability)//has enough ap for an ability
    {
        if(character.GetActionPoints() >= ability.ActionPointCost)
        {
            return true;
        }
        return false;
    }
    public bool HasSufficientAP(Vector3 destination)
    {
        return true;//TODO determine if we want to make it so they can click even when their path is too long (will only go to end of path being displayed)
    }
    public void CombatAbility(object sender, AbilityEventArgs e)
    {
        UpdateAbility(e.NewAbility);
    }
    public void CombatAbilityDeselect()
    {
        UpdateAbility(null);
        uiHandler.UnselectAbilities();
    }
    public void CombatTarget(Character target)
    {
        UpdateTarget(target);
    }
    public void ToggleCombatTurn()
    {
        attackingOrMoving = !attackingOrMoving;
        if (attackingOrMoving)
        {
            uiHandler.UpdateCombatTurnUI(character);
        }
        else{
            uiHandler.DisplayDoubleMovement(true);
            uiHandler.StopDisplayingAbilities();
        }
    }

    public void CombatMovement(Vector3 destination)
    {
        Vector3 characterBottom = character.CharacterBottom();
        Vector3 adjustedDestination = new Vector3(destination.x, destination.y , destination.z);
        NavMeshPath path = new NavMeshPath();
        if (character.Agent.CalculatePath(adjustedDestination, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            float pathLength = movementProcesssor.CalculatePathLength(path, character);
            if (pathLength <= character.GetMaxMovement())
            {
                //If the destination is valid, move to destination
                UpdateMovement(adjustedDestination - characterBottom, pathLength);
            }
            else
            {//move to closest point on destination (should be where path renderer is displayed last)
                float distanceTraveled = 0;
                Vector3 location = new Vector3();
                Vector3 prev = characterBottom;
                bool first = true;
                //prev.y += 0.08448386f;//TODO investigate why this helps eventually
                foreach (Vector3 vector in path.corners)
                {
                    if (distanceTraveled + Vector3.Distance(vector, prev) >= character.GetMaxMovement())
                    {
                        Vector3 temp = vector - prev;
                        Vector3 lastPath = (character.GetMaxMovement() - distanceTraveled) * (temp.normalized);
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
                    UpdateMovement(location, character.GetMaxMovement());
                    //UpdateIteration(new Turn(location), false);
                }
                else
                {
                    Debug.Log("error occured");//TODO figure out why this happens
                }
            }
        }
    }
    public void FinishTurn(object sender, EventArgs e)
    {
        IterateCharacters();
    }
    #endregion
}
