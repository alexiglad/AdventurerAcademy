using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameState/GameController")]
public class GameControllerSO : ScriptableObject
{
    GameController gameController;
    //public GameController getCurrent

    public GameController GetGameController()
    {
        return gameController;
    }
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }
}
