using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicAI
{
    protected GameStateManagerSO gameStateManager;
    protected MovementProcessor movementProcessor;

    public BasicAI()
    {
        GameStateManagerSO[]  temp = Resources.FindObjectsOfTypeAll<GameStateManagerSO>();
        MovementProcessor[] temp1 = Resources.FindObjectsOfTypeAll<MovementProcessor>();

        gameStateManager = temp[0];
        movementProcessor = temp1[0];
    }
    public Turn DetermineTurn(Character character)
    {
        if(gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();

            //selecting character
            List<Character> players = new List<Character>();
            foreach (Character characterE in tempRef.Characters)
            {
                if (characterE.IsPlayer())
                    players.Add(characterE);
            }
            int num2 = Random.Range(0, players.Count);
            Character target = players[num2];

            //selecting ability
            List<Ability> abilities = new List<Ability>();
            foreach (Ability ability in character.GetCharacterData().GetInUseAbilities())
            {
                if (movementProcessor.WithinRange(character, target, ability))
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
                character.Animator.SetBool("walking", true);
                Vector3 direction = target.transform.position - character.transform.position;//TODO add code to verify this movement
                Turn turn = new Turn(3*direction.normalized/* + character.transform.position*/);//move 1 tile towards selected character
                return turn;
            }
        }
        return null;
    }


}
