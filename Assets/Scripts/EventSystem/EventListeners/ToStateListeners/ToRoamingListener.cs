using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToRoamingListener : MonoBehaviour
{
    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] private GameStateSO currentGameState;
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
        if (e.NewBool)//true so means they won, act normally display win screen eventually
        {
            //display win screen code here
            currentGameStateManager.CreateStateInstance(GameStateEnum.Roaming, null);
        }
        else//lost so has to act accordingly; TEMP loading screen
        {
            currentGameStateManager.CreateStateInstance(GameStateEnum.Loading, null);
        }
    }
}
