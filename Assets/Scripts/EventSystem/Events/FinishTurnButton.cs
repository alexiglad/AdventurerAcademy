using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTurnButton : MonoBehaviour
{
    public event EventHandler OnFinishTurnButtonClicked;

    GameObject endTurn;

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
            this.gameObject.GetComponent<Image>().color = Color.white;
            endTurn.SetActive(false);
        }
    }
    public void StopDisplay()
    {
        endTurn.SetActive(false);
    }
    public void Display()
    {
        endTurn.SetActive(true);
    }
    public void OnFinishTurnButtonPressed()
    {
        OnFinishTurnButtonClicked?.Invoke(this, EventArgs.Empty);
    }
    public void OnEnter()
    {
        this.gameObject.GetComponent<Image>().color = Color.grey;
    }

    public void OnExit()
    {
        this.gameObject.GetComponent<Image>().color = Color.white;
    }
}
