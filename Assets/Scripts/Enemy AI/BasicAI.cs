using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BasicAI 
{
    [SerializeField] protected GameStateManagerSO gameStateManager;
    public Turn DetermineTurn(Character character)
    {
        CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();

        double temp1 = UnityEngine.Random.Range(0, character.InUseAbilities.Count - 1);
        int num1 = (int)Math.Round(temp1);
        Ability ability = character.InUseAbilities[num1];

        List<Character> players = new List<Character>();
        foreach (Character characterE in tempRef.characters)
        {
            if (characterE.GetPlayer())
                players.Add(characterE);
        }
        double temp2 = UnityEngine.Random.Range(0, players.Count - 1);
        int num2 = (int)Math.Round(temp2);
        Character target = players[num2];

        Turn turn = new Turn(ability, target);//ability need target code now
        return (turn);//no movement as of right now :/
        
    }


}
