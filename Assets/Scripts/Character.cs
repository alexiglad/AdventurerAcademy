using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IComparable<Character>
{
    #region Local Variables
    [SerializeField] protected FloatValueSO health;
    [SerializeField] private FloatValueSO energy;
    [SerializeField] protected FloatValueSO maxHealth;
    [SerializeField] private FloatValueSO maxEnergy;
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
    [SerializeField] private List<Ability> inUseAbilities = new List<Ability>();//size 5
    private List<Status> statuses = new List<Status>();

    protected Rigidbody2D characterRigidBody;
    [SerializeField] protected GameStateManagerSO gameStateManager;

    

    public List<Ability> InUseAbilities { get => inUseAbilities; set => inUseAbilities = value; }
    public BasicAI EnemyAI { get => enemyAI; set => enemyAI = value; }
    public StringValueSO Name { get => name; set => name = value; }
    public List<Status> Statuses { get => statuses; set => statuses = value; }


    #endregion

    public new String ToString()
    {
        return this.name.GetStringValue();
    }
    private void OnEnable()
    {
        FindObjectOfType<GameController>().AddCharacter(this);
        enemyAI = new BasicAI();
    }
    private void OnDisable()
    {
        //FindObjectOfType<GameController>().RemoveCharacter(this);
    }
    protected void Start()
    {
        //Resets Character's Health, Mana, and Stamina to maximum on runtime
        health.SetFloatValue(maxHealth.GetFloatValue());
        energy.SetFloatValue(maxEnergy.GetFloatValue());
        //damage.SetFloatValue(damage.GetFloatValue() + Mathf.Round(Random.Range(-1*damageRange.GetFloatValue(), +1*damageRange.GetFloatValue())));

    }
    

    public void Dead(){
        //create event?
        //TODO fix
        Debug.Log(this + " died!");
        if (gameStateManager.GetGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();
            tempRef.RemoveCharacter(this);
        }
        else
        {
            Debug.Log("problem occured");
        }
        //add more for when player is dead
        //todo determine if need following destroy code
        //Destroy(this);
    }

    #region Getters and Setters
    public bool IsPlayer() {
        return isPlayer.GetBoolValue();
    }
    public FloatValueSO GetHealth() {
        return health;
    }
    public FloatValueSO GetMaxHealth()
    {
        return maxHealth;
    }
    public FloatValueSO GetEnergy()
    {
        return energy;
    }
    public FloatValueSO GetMaxEnergy()
    {
        return maxEnergy;
    }
    public float GetMaxMovement()
    {
        return maxMovement.GetFloatValue();
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed.GetFloatValue();
    }

    public string GetName()
    {
        return name.GetStringValue();
    }
    public Rigidbody2D GetCharacterRigidBody()
    {
        return characterRigidBody;
    }

    public virtual void SetHealth(float value)
    {
        health.SetFloatValue(value);
        if (this.health.GetFloatValue() <= 0)
        {
            this.Dead();
        }
    }
    public bool DecrementHealth(float value)
    {
        Debug.Log("Decremented " + this + " health by: " + value);
        health.SetFloatValue(health.GetFloatValue() - value);
        if (this.health.GetFloatValue() <= 0)
        {
            this.Dead();
            return true;//true means dead
        }
        return false;
    }
    public virtual void IncrementHealth(float value)
    {
        health.SetFloatValue(health.GetFloatValue() + value);
    }
    public float GetPercentHealth()
    {
        return this.health.GetFloatValue() / this.maxHealth.GetFloatValue();
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