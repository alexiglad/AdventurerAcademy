using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicAI 
{
    [SerializeField] protected GameStateManagerSO gameStateManager;
    public Turn DetermineTurn(Character character)
    {
        CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();

        int num1 = Random.Range(0, character.InUseAbilities.Count - 1);
        Ability ability = character.InUseAbilities[num1];

        List<Character> players = new List<Character>();
        foreach (Character characterE in tempRef.characters)
        {
            if (characterE.GetPlayer())
                players.Add(characterE);
        }
        int num2 = Random.Range(0, players.Count - 1);
        Character target = players[num2];

        Turn turn = new Turn(ability, target);//ability need target code now
        return (turn);//no movement as of right now :/
        
    }


}
