using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IComparable<Character>
{
    #region Local Variables

    [SerializeField] private BasicAI enemyAI;
    [SerializeField] private CharacterData characterData;
    [SerializeField] GameStateManagerSO gameStateManager;

    [SerializeField] protected float health;
    private float energy;

    private List<Status> statuses = new List<Status>();

    protected Rigidbody2D characterRigidBody;

    NavMeshAgent agent;
    
    public BasicAI EnemyAI { get => enemyAI; set => enemyAI = value; }    
    public List<Status> Statuses { get => statuses; set => statuses = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }

    #endregion

    public new String ToString()
    {
        return this.characterData.GetName();
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
        //Resets Character's Health,and Energy to maximum on runtime
        health = characterData.GetMaxHealth();
        energy = characterData.GetMaxEnergy();
        agent = GetComponent<NavMeshAgent>();
        //damage.SetFloatValue(damage.GetFloatValue() + Mathf.Round(Random.Range(-1*damageRange.GetFloatValue(), +1*damageRange.GetFloatValue())));
    }
    

    public void Dead(){
        //create event?
        //TODO fix
        Debug.Log(this + " died!");
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
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
        return characterData.IsPlayer();
    }
    public float GetHealth() {
        return health;
    }
    public float GetMaxHealth()
    {
        return characterData.GetMaxHealth();
    }
    public float GetEnergy()
    {
        return energy;
    }
    public float GetMaxEnergy()
    {
        return characterData.GetMaxEnergy();
    }
    public float GetMaxMovement()
    {
        return characterData.GetMaxMovement();
    }
    
    public float GetMoveSpeed()
    {
        return characterData.GetMoveSpeed();
    }

    public string GetName()
    {
        return characterData.GetName();
    }
    public Rigidbody2D GetCharacterRigidBody()
    {
        return characterRigidBody;
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
    }

    public virtual void SetHealth(float value)
    {
        health = value;
        if (this.health <= 0)
        {
            this.Dead();
        }
    }
    public bool DecrementHealth(float value)
    {
        Debug.Log("Decremented " + this + " health by: " + value);
        health -= value;
        if (this.health <= 0)
        {
            this.Dead();
            return true;//true means dead
        }
        return false;
    }
    public virtual void IncrementHealth(float value)
    {
        health += value;
    }
    public float GetPercentHealth()
    {
        return this.health / this.characterData.GetMaxHealth();
    }
    #endregion
    public int CompareTo(Character character)//test if this is actually working
    {
        if (this.characterData.GetInitiative() - character.characterData.GetInitiative() < 0)
            return 1;
        else if (this.characterData.GetInitiative() - character.characterData.GetInitiative() > 0)
            return -1;
        else
            return base.GetHashCode().CompareTo(character.GetHashCode());
    }

}