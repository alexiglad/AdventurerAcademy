using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Character : MonoBehaviour, IComparable<Character>
{
    #region Local Variables


    [SerializeField] private BasicAI enemyAI;
    [SerializeField] private CharacterData characterData;
    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] FollowUpProcessor followUpProcessor;
    [SerializeField] CharacterListSO characterList;

     
    [SerializeField] protected float health;
    [SerializeField] protected float energy; 
    private bool revived;
    private bool died;
    private bool unstable;
    private bool moving;
    Vector3 lastPos;
    Vector3 previousSteeringTarget;
    int movementIdleCounter;
    [SerializeField] private bool inanimate;
    private Character voodooTarget;
    

    Animator animator;
    CardinaDirectionsEnum direction;

    BoxCollider boxCollider;

    [SerializeField] private List<Status> statuses = new List<Status>();

    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    SpriteRenderer spriteRenderer;

    List<Interactable> interactablesWithinRange;


    public BasicAI EnemyAI { get => enemyAI; set => enemyAI = value; }    
    public List<Status> Statuses { get => statuses; set => statuses = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public BoxCollider BoxCollider { get => boxCollider; set => boxCollider = value; }
    public bool Revived { get => revived; set => revived = value; }
    public Character VoodooTarget { get => voodooTarget; set => voodooTarget = value; }
    public bool Inanimate { get => inanimate; set => inanimate = value; }
    public NavMeshObstacle Obstacle { get => obstacle; set => obstacle = value; }
    public CardinaDirectionsEnum Direction { get => direction; set => direction = value; }
    public List<Interactable> InteractablesWithinRange { get => interactablesWithinRange; set => interactablesWithinRange = value; }
    public bool Unstable { get => unstable; set => unstable = value; }
    public bool Moving { get => moving; set => moving = value; }
    public Vector3 PreviousSteeringTarget { get => previousSteeringTarget; set => previousSteeringTarget = value; }

    #endregion
    public new String ToString()
    {
        return this.characterData.GetName();
    }
    public void ManualAwake()
    {
        //this.Awake();
        this.gameObject.SetActive(true);
        agent = transform.GetComponent<NavMeshAgent>();
        obstacle = transform.GetComponent<NavMeshObstacle>();
        direction = CardinaDirectionsEnum.South;
        agent.enabled = false;
        obstacle.enabled = true;
        //Debug.Log()
        //transform.GetComponent<SpriteRenderer>().materials
        this.Start();
    }
    private void Awake()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        obstacle = transform.GetComponent<NavMeshObstacle>();
        direction = CardinaDirectionsEnum.South;
        if (!inanimate)
        {
            characterList.AddCharacter(this);
            enemyAI = new BasicAI();
            agent.enabled = false;
            obstacle.enabled = true;
        }
    }

    protected void Start()
    {
        //Resets Character's Health,and Energy to maximum on runtime
        health = characterData.GetMaxHealth();
        energy = characterData.GetMaxEnergy();
        revived = false;
        died = false;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        interactablesWithinRange = new List<Interactable>();
    }

    void LateUpdate()
    {
        if (!inanimate)
        {
            Vector3 realPos = this.BoxCollider.bounds.center;
            realPos.y -= this.BoxCollider.bounds.size.y / 2;
            if (Vector3.Distance(realPos, agent.destination) <= .2f)
            {
                movementIdleCounter = 0;
                if (animator.GetBool("moving"))
                {
                    moving = false;
                    StopMoving();
                    if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
                    {
                        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                        if (this == tempRef.Character)
                        {
                            tempRef.EnableCombatInput();
                        }
                    }
                    else if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(RoamingManager))
                    {
                        RoamingManager tempRef = (RoamingManager)gameStateManager.GetCurrentGameStateManager();
                        if (this == tempRef.Character)
                        {
                            tempRef.EnableRoamingInput();
                        }
                    }
                }

            }
            else if (agent.enabled)
            {
                if(agent.steeringTarget != previousSteeringTarget)
                {
                    Vector3 steeringTarget = Agent.steeringTarget;
                    if((steeringTarget - CharacterBottom()).magnitude <= 1)
                    {

                        Debug.Log("here");
                        //Animator.SetBool("running", false);
                        Animator.SetBool("walking", true);
                    }
                    else
                    {
                        //Animator.SetBool("walking", false);
                        Animator.SetBool("running", true);
                    }
                    float xChange = steeringTarget.x - CharacterBottom().x;
                    float zChange = steeringTarget.z - CharacterBottom().z;
                    Animator.SetFloat("moveX", xChange);
                    Animator.SetFloat("moveZ", zChange);
                    previousSteeringTarget = agent.steeringTarget;
                }
                if (IsStationary())
                {
                    movementIdleCounter++;
                    if (movementIdleCounter > 1000)//figure this number out
                    {
                        movementIdleCounter = 0;
                        StopMoving();
                        agent.SetDestination(CharacterBottom());

                        Debug.Log("ERROR failsafe activated please investigate");
                        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
                        {
                            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                            tempRef.EnableCombatInput();
                        }
                    }
                }
                else
                {
                    movementIdleCounter = 0;
                    moving = true;
                    followUpProcessor.HandleFollowUpAction(new FollowUpAction(this, GetComponent<NavMeshAgent>().velocity));
                }
            }
            else
            {//todo figure out why the hell this is here
                movementIdleCounter = 0;//todo check for optimization
                if (animator.GetBool("moving"))
                {
                    StopMoving();
                    Debug.Log("error please investigate");
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactablesWithinRange.Add(interactable);
        }
        else
        {
            //Debug.Log("error occcured");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (!interactablesWithinRange.Remove(interactable))
            {
                Debug.Log("error occured");
            }
        }
        else
        {
            //Debug.Log("error occcured");
        }
    }
    void StopMoving()
    {
        animator.SetBool("moving", false);

        if (animator.GetBool("walking"))
        {
            animator.SetBool("walking", false);
        }
        else if(animator.GetBool("running"))
        {
            animator.SetBool("running", false);
        }
        else
        {
            Debug.Log("Error please investigate");
        }
    }
    bool IsStationary()
    {
        bool returning =  Vector3.Distance(this.transform.position, lastPos) <= .01;
        lastPos = this.transform.position;
        return returning;
    }
    public Vector3 CharacterBottom()
    {
        Vector3 characterBottom = this.BoxCollider.bounds.center;
        characterBottom.y -= this.BoxCollider.bounds.size.y / 2;
        return characterBottom;
    }
    public void ResetCharacter()
    {
        //todo determine other functionality of this method
        this.statuses.Clear();
    }
    public void Dead(){
        //create event?
        if (died)
        {
            return;
        }
        //Debug.Log(this + " died!");
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
                this.GetComponent<SpriteRenderer>().color = Color.white;
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                tempRef.RemoveCharacter(this);
                died = true;
            }                
        }
        else
        {
            Debug.Log("problem occured");
        }
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
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.AddDamagedCharacter(new DamageData(value - health, this));
        }
        health = value;
        if (this.health <= 0)
        {
            this.Dead();
        }
    }
    public bool DecrementHealth(float value)
    {
        health -= value;
        if(gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.AddDamagedCharacter(new DamageData(-value, this));
        }
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
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.AddDamagedCharacter(new DamageData(value, this));
        }
    }
    public float GetPercentHealth()
    {
        return this.health / this.characterData.GetMaxHealth();
    }
    #endregion
    #region ShaderManipulation
    public void SetSpriteShader(SpriteShaderTypeEnum spriteShader)
    {
        switch (spriteShader)
        {
            case SpriteShaderTypeEnum.Damage:
                {

                    break;
                }
            case SpriteShaderTypeEnum.Heal:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Selected:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Unable:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Death:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Regen:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Burn:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Poison:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Frozen:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Sleep:
                {


                    break;
                }
            case SpriteShaderTypeEnum.Knocked:
                {

                    break;
                }
            case SpriteShaderTypeEnum.Drunk:
                {

                    break;
                }
            case SpriteShaderTypeEnum.Blind:
                {

                    break;
                }
            case SpriteShaderTypeEnum.None:
                {
                    StopAllShaders();
                    break;
                }
            default:
                {
                    Debug.Log("ERROR - add shader to enum and case list");
                    StopAllShaders();
                    break;
                }

        }
    }




    public void StopAllShaders()
    {
        Debug.Log("HERE " + spriteRenderer.material);
        spriteRenderer.material.SetFloat(ShaderInformation.doFlash, 0);
        spriteRenderer.material.SetFloat(ShaderInformation.flashOrPulsate, 0);
        spriteRenderer.material.SetFloat(ShaderInformation.doSelection, 0);
        spriteRenderer.material.SetFloat(ShaderInformation.grayOut, 0);
        spriteRenderer.material.SetFloat(ShaderInformation.fadeAmount, 0);
        spriteRenderer.material.SetFloat(ShaderInformation.doShake, 0);
    }
    public void DisplayStatusShader()
    {
        StartCoroutine(StatusShaderCoroutine());
    }
    IEnumerator StatusShaderCoroutine()
    {
        spriteRenderer.material.SetFloat(ShaderInformation.flashSpeed, 1);
        spriteRenderer.material.SetFloat(ShaderInformation.doFlash, 1);
        yield return new WaitForSeconds(1);
        StopAllShaders();
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.EnableCombatInput();
        }
        else
        {
            Debug.Log("combat should be over if not then error");
        }
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