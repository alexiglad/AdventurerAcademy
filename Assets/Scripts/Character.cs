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
    [SerializeField] FollowUpProcessor followUpProcessor;


    [SerializeField] protected float health;
    [SerializeField] protected float energy;    
    private bool revived;
    [SerializeField] private bool inanimate;
    private Character voodooTarget;
    

    Animator animator;

    BoxCollider boxCollider;

    private List<Status> statuses = new List<Status>();

    NavMeshAgent agent;
    
    public BasicAI EnemyAI { get => enemyAI; set => enemyAI = value; }    
    public List<Status> Statuses { get => statuses; set => statuses = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public BoxCollider BoxCollider { get => boxCollider; set => boxCollider = value; }
    public bool Revived { get => revived; set => revived = value; }
    public Character VoodooTarget { get => voodooTarget; set => voodooTarget = value; }
    public bool Inanimate { get => inanimate; set => inanimate = value; }

    #endregion

    public new String ToString()
    {
        return this.characterData.GetName();
    }
    public void Update()
    {
        
    }
    private void OnEnable()
    {
        if(!inanimate)
        {
            FindObjectOfType<GameController>().AddCharacter(this);
            enemyAI = new BasicAI();
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            this.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
        }

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
        revived = false;
        agent = transform.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        //damage.SetFloatValue(damage.GetFloatValue() + Mathf.Round(Random.Range(-1*damageRange.GetFloatValue(), +1*damageRange.GetFloatValue())));
    }

    void LateUpdate()
    {
        if (!inanimate)
        {
            Vector3 realPos = this.BoxCollider.bounds.center;
            realPos.y -= this.BoxCollider.bounds.size.y / 2;
            if (Vector3.Distance(realPos, agent.destination) <= .2f)
            {
                if (animator.GetBool("walking")){
                    animator.SetBool("walking", false);
                    if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
                    {
                        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                        if (this == tempRef.Character)
                        {
                            tempRef.CanContinue = true;
                        }
                    }
                }
                
            }
            else
            {
                followUpProcessor.HandleFollowUpAction(new FollowUpAction(this, GetComponent<NavMeshAgent>().velocity));
                //a way to use this is if(Vector3.Angle(velocity vector, target character))
            }
        }
        
    }


    public void Dead(){
        //create event?
        //TODO fix
        Debug.Log(this + " died!");
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            bool revive = false;
            foreach(FollowUp followUp in this.characterData.GetFollowUps())//TODO make this more efficient
            {
                if(followUp.FollowUpType == FollowUpTypeEnum.Death && !this.Revived)
                {
                    revive = true;
                    this.Revived = true;
                }
            }
            if (revive) 
            {
                followUpProcessor.HandleFollowUpAction(new FollowUpAction(this));
            }
            else
            {
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.RemoveCharacter(this);
            }                
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
    public bool DifferentSides(Character character)
    {
        return this.IsPlayer() ^ character.IsPlayer();
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

    public CharacterData GetCharacterData()
    {
        return characterData;
    }
    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;
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