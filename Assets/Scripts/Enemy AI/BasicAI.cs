using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicAI
{

    public BasicAI()
    {
        //here we will use the character data to decide which neural network to use
    }
    public Turn DetermineTurn(Character character, CombatManager combatManager)
    {

        //selecting character
        List<Character> players = new List<Character>();
        foreach (Character characterE in combatManager.Characters)
        {
            if (characterE.IsPlayer())
                players.Add(characterE);
        }
        if (players.Count == 0)
        {
            Debug.Log("ERROR NO USERS!");
            return null;
        }
        int num2 = Random.Range(0, players.Count);
        Character target = players[num2];

        //selecting ability
        List<Ability> abilities = new List<Ability>();
        foreach (Ability ability in character.GetCharacterData().GetInUseAbilities())
        {
            if (WithinRange(character, target, ability))
            {
                abilities.Add(ability);
            }
        }
        if (abilities.Count != 0)
        {
            int num1 = Random.Range(0, abilities.Count);
            Ability abilitySelected = character.GetCharacterData().GetInUseAbilities()[num1];
            Turn turn = new Turn(abilitySelected, target);
            return turn;
        }
        else
        {
            Vector3 direction = target.transform.position - character.transform.position;
            direction.y -= DetermineDifferenceInHeights(target, character);
            //TODO finish evening out heights (need cedric to make it so gravity works at start)
            Turn turn = new Turn(2 * direction.normalized/* + character.transform.position*/);//move 1 tile towards selected character
            return turn;
        }
    }
    public bool WithinRange(Character character, Character target, Ability ability)
    {
        return Vector3.Distance(character.transform.position, target.transform.position) <= ability.Range;
    }
    public float DetermineDifferenceInHeights(Character character1, Character character2)
    {
        float character1y = character1.transform.position.y - character1.BoxCollider.bounds.size.y / 2;
        float character2y = character2.transform.position.y - character2.BoxCollider.bounds.size.y / 2;
        return character1y - character2y;
    }
}
