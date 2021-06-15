using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IComparable<Character>
{
    #region Local Variables
    [SerializeField] protected FloatValueSO health;
    [SerializeField] private FloatValueSO mana;
    [SerializeField] private FloatValueSO stamina;
    [SerializeField] protected FloatValueSO maxHealth;
    [SerializeField] private FloatValueSO maxMana;
    [SerializeField] private FloatValueSO maxStamina;
    [SerializeField] private BoolValueSO isPlayer;//true if player false if enemy
    [SerializeField] protected FloatValueSO maxMovement;//this eventually will be calculated based on speed/agility and what-not
    [SerializeField] protected FloatValueSO initiative;
    [SerializeField] protected FloatValueSO moveSpeed;
    [SerializeField] private new StringValueSO name;

    [SerializeField] private FloatValueSO damage;
    [SerializeField] private FloatValueSO damageMultiplier;

    [SerializeField] protected FloatValueSO damageRange;
    [SerializeField] private BasicAI enemyAI;



    public List<FollowUp> followUps = new List<FollowUp>();
    private List<Ability> inUseAbilities = new List<Ability>();//size 5
    private List<Status> statuses = new List<Status>();

    protected Rigidbody2D characterRigidBody;
    protected Vector2 movementLeft;
    [SerializeField] protected MovementProcessor abilityProcessorInstance;
    [SerializeField] protected FollowUpProcessor followUpProcessorInstance;
    [SerializeField] protected GameStateManagerSO gameStateManager;

    

    public List<Ability> InUseAbilities { get => inUseAbilities; set => inUseAbilities = value; }
    public BasicAI EnemyAI { get => enemyAI; set => enemyAI = value; }
    public StringValueSO Name { get => name; set => name = value; }
    public List<Status> Statuses { get => statuses; set => statuses = value; }


    #endregion
    public Character(string name, float initiative){//Note this is not being called. Marked for future removal
        this.name.SetStringValue(name);
        this.initiative.SetFloatValue(initiative);
    }
    public new String ToString()
    {
        return this.name.GetStringValue();
    }
    private void OnEnable()
    {
        abilityProcessorInstance = (MovementProcessor)MovementProcessor.FindObjectOfType(typeof(MovementProcessor));
        followUpProcessorInstance = (FollowUpProcessor)FollowUpProcessor.FindObjectOfType(typeof(FollowUpProcessor));
        FindObjectOfType<GameController>().AddCharacter(this);
    }
    private void OnDisable()
    {
        //FindObjectOfType<GameController>().RemoveCharacter(this);
    }
    protected void Start()
    {
        //Resets Character's Health, Mana, and Stamina to maximum on runtime
        health.SetFloatValue(maxHealth.GetFloatValue());
        mana.SetFloatValue(maxMana.GetFloatValue());
        stamina.SetFloatValue(maxStamina.GetFloatValue());
        //damage.SetFloatValue(damage.GetFloatValue() + Mathf.Round(Random.Range(-1*damageRange.GetFloatValue(), +1*damageRange.GetFloatValue())));

    }
    
    public void CreateUI()//creates ability buttons
    {
        foreach(Ability ability in inUseAbilities)
        {
            //create button TODO
            
        }
    }


    public bool InflictAbility(Character attackee, Ability ability){        
        
        ability.HandleAbility(this, attackee, ability);//format is always attacker, attackee
        //must check follow up after every ability
        followUpProcessorInstance.HandleFollowUpAction(new FollowUpAction(this, attackee, ability));
        return true;
    }
    public void Dead(){
        //create event?
        //combatManagerInstance.characters.Remove(this);//Alexi code
        if (gameStateManager.GetGameStateManager().GetType() == typeof(CombatManager))//Cedric Change
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();
            tempRef.RemoveCharacter(this);
        }
        //add more for when player is dead
    }

    #region Getters and Setters
    public bool GetPlayer() {
        return isPlayer;
    }
    public FloatValueSO GetHealth() {
        return health;
    }
    public FloatValueSO GetMaxHealth()
    {
        return maxHealth;
    }
    public FloatValueSO GetMana()
    {
        return mana;
    }
    public FloatValueSO GetMaxMana()
    {
        return maxMana;
    }
    public FloatValueSO GetStamina()
    {
        return stamina;
    }
    public FloatValueSO GetMaxStamina()
    {
        return maxStamina;
    }
    public float GetMaxMovement()
    {
        return maxMovement.GetFloatValue();
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed.GetFloatValue();
    }
    public Rigidbody2D GetCharacterRigidBody()
    {
        return characterRigidBody;
    }
    public void SetMovement(Vector2 movement)
    {
        movementLeft = movement;
    }

    public virtual void SetHealth(float value)
    {
        health.SetFloatValue(value);
    }
    public virtual void DecrementHealth(float value)
    {
        health.SetFloatValue(health.GetFloatValue() - value);
    }
    public virtual void IncrementHealth(float value)
    {
        health.SetFloatValue(health.GetFloatValue() + value);
    }
    #endregion
    public int CompareTo(Character character)//test if this is actually working
    {
        if (this.initiative.GetFloatValue() - character.initiative.GetFloatValue() < 0)
            return 1;
        else if (this.initiative.GetFloatValue() - character.initiative.GetFloatValue() > 0)
            return -1;
        else
            return base.GetHashCode().CompareTo(character.GetHashCode());
    }

}