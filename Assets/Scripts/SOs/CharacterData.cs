using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] new string name;

    [SerializeField] float maxHealth;
    [SerializeField] float maxEnergy;

    [SerializeField] float maxMovement;//this eventually will be calculated based on speed/agility and what-not
    [SerializeField] float moveSpeed;

    [SerializeField] float initiative;
    
    [SerializeField] float damage;
    [SerializeField] float damageMultiplier;
    [SerializeField] float damageRange;

    [SerializeField] protected List<Ability> inUseAbilities = new List<Ability>();//size 5
    [SerializeField] List<FollowUp> followUps = new List<FollowUp>();

    [SerializeField] bool isPlayer;

    [SerializeField] Sprite portrait;


    #region Getters and Setters

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    public float GetMaxMovement()
    {
        return maxMovement;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public string GetName()
    {
        return name;
    }

    public float GetInitiative()
    {
        return initiative;
    }

    public List<FollowUp> GetFollowUps()
    {
        return followUps;
    }

    public List<Ability> GetInUseAbilities()
    {
        return inUseAbilities;
    }

    public List<Ability> InUseAbilities { get => inUseAbilities; set => inUseAbilities = value; }
    public string Name { get => name; set => name = value; }
    public Sprite Portrait { get => portrait; set => portrait = value; }
    #endregion
}