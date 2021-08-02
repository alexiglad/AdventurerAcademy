using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameState/GameState")]
public class GameStateSO : ScriptableObject
{
    [SerializeField] private GameStateEnum currentGameState;
    private void Awake()
    {
        currentGameState = GameStateEnum.Roaming;
    }
    public GameStateEnum GetGameState()
    {
        return currentGameState;
    }

    public void SetGameState(GameStateEnum state)
    {
        currentGameState = state;
    }
}
