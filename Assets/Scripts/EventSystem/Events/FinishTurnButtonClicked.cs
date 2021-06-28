using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTurnButtonClicked : MonoBehaviour
{
    public event EventHandler OnFinishTurnButtonClicked;

    public GameObject endTurn;

    public void ManualAwake()
    {
        endTurn = GameObject.Find("EndTurn");
    }
    public void UpdateButton(bool isPlayer)
    {
        if (isPlayer)
        {
            endTurn.SetActive(true);
        }
        else
        {
            
            endTurn.SetActive(false);
        }
    }
    public void OnFinishTurnButtonPressed()
    {
        OnFinishTurnButtonClicked?.Invoke(this, EventArgs.Empty);
    }
}
