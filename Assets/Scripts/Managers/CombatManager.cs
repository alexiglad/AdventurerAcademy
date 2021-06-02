using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : GameStateManager
{
    #region Local Variables
    public SortedSet<Character> characters = new SortedSet<Character>();
    private IEnumerator<Character> enumerator;
    bool playerIsAlive;
    bool characterType;
    Turn turn;
    Turn turnChange;
    Player player = null;//TODO set this equal to player instance
    Character character;

    #endregion
    void Awake(){
        //set up local variables here
        playerIsAlive = true;
        enumerator = characters.GetEnumerator();
        character = enumerator.Current;
        //TODO UNCOMMENT THIS WHEN FIXED
        //characterType = GetCharacterType();//true if the character is player side false if enemy side
        turnChange = new Turn();

    }


    // Update is called once per frame
    
    public override void Update()
    {

        
        if (!playerIsAlive || !MoreThanOneSideIsAlive())
        {
            //trigger win/loss event TODO            
            //delete CombatManager instance and display battle won/lost
            //screen accordingly

        }

        if (!characterType) 
        {
            turnChange = DetermineEnemyTurn(character);
        }
        else
        {
            turnChange = DeterminePlayerTurn(character);
        }

        if (!turnChange.IsEmpty()) { 
            UpdateTurn(turnChange);//this just adjusts the global variable turn appropriately
            if (ValidTurn(turnChange))
            {
                UpdateCharacters(turnChange);
            }
            
        }
        
        if (TurnFinished())
            IterateCharacters();
    }
    public Turn DetermineEnemyTurn(Character character)//TODO
    {
        return null;//implement event listeners
    }
    public Turn DeterminePlayerTurn(Character character)//TODO
    {
        return null;//implement event listeners
    }
    public void UpdateTurn(Turn turnChange)
    {
        if (turnChange.GetMovement() != Vector2.zero)
        {//add x and y components to turn
            turn.SetMovement(turnChange.GetMovement() + turn.GetMovement());
        }
        else if(turnChange.GetAbility() != null)
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
    

    bool GetCharacterType(){
        return character.GetPlayer();
    }

    bool ValidTurn(Turn pushTurn){
        
        if(pushTurn.GetMovement()!=null){
            return true;
        }
        else if(turn.GetAbility() != null && turn.GetTarget() != null){
            return true;
        }
        else{
            return false;
        }
    }

    bool TurnFinished(){
        if(turn.GetMovement().magnitude>=character.GetMaxMovement() && turn.GetAbility()!=null && turn.GetTarget()!=null)
        {
            return true;
        }
        else if(false){//TODO get event listener input for "finish turn" button pressed
        #pragma warning disable 
            return true;
        #pragma warning restore
        }
        else{
            return false;
        }
    }

    public void RemoveCharacter(Character character) {
        characters.Remove(character);
        if(character == player)
            playerIsAlive=false;//pop up menu immediately
        //decide if whole squad is dead
    }

    void IterateCharacters(){
        turn = null;
        if(enumerator.MoveNext()){
            character = enumerator.Current;
            characterType=GetCharacterType();
        }
        else{//restart iteration through set however you do that
            character = enumerator.Current;
            characterType=GetCharacterType();	
        }
        turn = null;
    }
    bool MoreThanOneSideIsAlive()
    {
        int zero = 1;//used to check for f first iteration is done yet
        bool initial = false ;
        //private IEnumerator<Character> tempEnum = characters.GetEnumerator();


        foreach(Character character in characters)
        {
            if (zero==1)
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
    public override void AddCharacters(SortedSet<Character> characters)
    {
        this.characters = characters;
    }

}
