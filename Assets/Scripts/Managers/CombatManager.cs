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
        Debug.Log(character.name + " 's Turn!");
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
        return this.character.GetMaxMovement() - this.turn.AmountMoved;
    }

    public bool UpdateTurn(Turn turnChange)
    {//TODO check if movement and ability are valid additions to the turn
        bool updated = false;
        if (turnChange.GetMovement() != Vector3.zero && turn.AmountMoved <= character.GetMaxMovement())
        {//add x and y components to turn only if the movement is less than the max movement
            turn.SetMovement(turnChange.GetMovement() + turn.GetMovement());//all turn.movement does is just store the total movement done on a given turn
            turn.AmountMoved += Math.Abs(turnChange.GetMovement().magnitude - character.transform.position.magnitude);
            //turn.AmountMoved += Vector3.Distance(turnChange.GetMovement(), character.transform.position);
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
        if (pushTurn.GetMovement() != Vector3.zero)
        {
            return true;
        }
        if (turn.GetAbility() != null && turn.GetTarget() != null)
        {
            character.Animator.SetBool("walking", false);
            return true;
        }       
        character.Animator.SetBool("walking", false);
        Debug.Log("Invalid Turn");
        return false;        
    }

    bool TurnFinished()
    {
        if (turn.AmountMoved >= character.GetMaxMovement() && turn.GetAbility() != null && turn.GetTarget() != null)
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
                Debug.Log(character.name + "'s Turn!");
            }
            else
            {//TODO check if this works
                enumerator.Reset();
                enumerator.MoveNext();
                character = enumerator.Current;
                characterType = GetCharacterType();
                Debug.Log(character.name + "'s Turn!");
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
        Debug.Log("destination is" + destination);
        destination = new Vector3(destination.x, destination.y + character.GetCharacterData().YOffset, destination.z);
        NavMeshPath path = new NavMeshPath();
        if (character.Agent.CalculatePath(destination, path) && path.status == NavMeshPathStatus.PathComplete) 
        {
            if (Vector3.Distance(destination, character.transform.position) <= GetRemainingMovement())
            {
                UpdateIteration(new Turn(destination));

            }
            else
            {
                float distanceTraveled = 0;
                Vector3 location = new Vector3();
                Vector3 prev = character.transform.position;
                prev.y -= character.GetCharacterData().YOffset;
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
                NavMeshPath path2 = new NavMeshPath();
                if (character.Agent.CalculatePath(location, path2) && path2.status == NavMeshPathStatus.PathComplete)
                {
                    //this is creating the movement in the case of being outside the radius
                    UpdateIteration(new Turn(location));
                }

            }
        }
    }
    public void DisplayPath(NavMeshPath path)
    {
        List<Vector3> validPath = new List<Vector3>();
        List<Vector3> invalidPath = new List<Vector3>();
        float distanceTraveled = 0;
        bool over = false;
        for(int i=0; i<path.corners.Length; i++)
        {
            if (over)
            {
                invalidPath.Add(path.corners[i]);
            }
            else if (distanceTraveled + path.corners[i].magnitude > GetRemainingMovement())
            {
                Vector3 temp = path.corners[i];
                path.corners[i].Normalize();
                Vector3 lastValidPath = (GetRemainingMovement() - distanceTraveled) * path.corners[i];
                validPath.Add(lastValidPath);
                Vector3 firstInvalidPath = temp - lastValidPath;
                Debug.Log("first invalid path is: " + firstInvalidPath);
                invalidPath.Add(firstInvalidPath);
                over = true;
                //Debug.Log("last path is" + lastPath);
                //create new path based on paths used
            }
            else
            {
                distanceTraveled += path.corners[i].magnitude;
                validPath.Add(path.corners[i]);
                //Debug.Log(path.corners[i]);
            }
        }
        

    }
    void FinishTurn(object sender, EventArgs e)
    {
        IterateCharacters();
    }




    #endregion
}
