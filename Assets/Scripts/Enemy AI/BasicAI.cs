using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicAI
{
    [SerializeField] protected GameStateManagerSO gameStateManager;
    public BasicAI()
    {
        GameStateManagerSO[]  temp = Resources.FindObjectsOfTypeAll<GameStateManagerSO>();
        gameStateManager = temp[0];
    }
    public Turn DetermineTurn(Character character)
    {
        CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();

        int num1 = Random.Range(0, character.GetCharacterData().GetInUseAbilities().Count);
        Ability ability = character.GetCharacterData().GetInUseAbilities()[num1];

        List<Character> players = new List<Character>();
        foreach (Character characterE in tempRef.Characters)
        {
            if (characterE.IsPlayer())
                players.Add(characterE);
        }
        int num2 = Random.Range(0, players.Count);
        Character target = players[num2];

        Turn turn = new Turn(ability, target);//ability need target code now
        return (turn);//no movement as of right now :/
        
    }


}
