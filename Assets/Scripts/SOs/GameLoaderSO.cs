using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Loading/GameLoader")]
public class GameLoaderSO : ScriptableObject
{
    GameLoader gameLoader;

    public GameLoader GetGameLoader()
    {
        return gameLoader;
    }
    public void SetGameLoader(GameLoader gameLoader)
    {
        this.gameLoader = gameLoader;
    }
}
