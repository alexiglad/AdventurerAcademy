using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Voodoo allows a caster to place a voodoo doll and bind it to an enemy, allowing abilities and Follow-ups to be used on the 
/// doll with the damage transfering to the target enemy. The doll has a set ammount of HP and
/// cannot be moved once created.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Voodoo")]
public class AVoodoo : Ability
{
    [SerializeField] private GameObject voodooPrefab;
    [SerializeField] private CharacterData characterData;
    public Character target;
    private void OnEnable()
    {
        damage = 0;
        range = 10;
    }

    public override void HandleAbility(Character attacker, Character attackee, Ability ability)
    {
        //display voodoo doll on witch
        target = attackee;
        GameObject temp2 = Instantiate(voodooPrefab, attacker.transform.position, Quaternion.identity);
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        /*GameObject temp2 = new GameObject("Character");
        Character temp1;
        temp2.transform.position = attacker.transform.position;
        temp1 = temp2.AddComponent<Character>();*/
        Character temp1 = temp2.AddComponent<Character>();

        temp1.name = "voodoo";
        temp1.SetCharacterData(characterData);
        temp1.VoodooTarget = target;
        tempRef.Characters.Add(temp1);//todo check if this works
        tempRef.ResetEnumerator();
    }
}