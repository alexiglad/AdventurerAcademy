using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToRoamingListener : MonoBehaviour
{
    [SerializeField] private GameStateManagerSO gameStateManager;
    [SerializeField] private GameStateSO gameState;
    // Start is called before the first frame update
    void Start()
    {
        //ToRoaming from Combat
        CombatOver onCombatOver = FindObjectOfType<CombatOver>();
        onCombatOver.OnCombatOver += ToRoaming;


        //ToRoaming from Dialogue


        //etc continue this for every overload
    }

    void ToRoaming(object sender, BoolEventArgs e)
    {
        SortedSet<Character> userCharacters = new SortedSet<Character>();
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            userCharacters = tempRef.UserCharacters;
        }
        else
        {
            Debug.Log("problem occured");
        }
        if (e.NewBool)//true so means they won, act normally display win screen eventually
        {
            //display win screen code here
            gameStateManager.CreateStateInstance(GameStateEnum.Roaming, userCharacters);
        }
        else//lost so has to act accordingly; TEMP back to roaming with no repercussions
        {
            gameStateManager.CreateStateInstance(GameStateEnum.Roaming, userCharacters);
        }
    }
}
