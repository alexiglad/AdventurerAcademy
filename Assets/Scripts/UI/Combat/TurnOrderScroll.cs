using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderScroll : MonoBehaviour
{
    //Increase x while decreasing width. Do opposite for unfurling
    //
    Transform scroll;
    Transform container;
    List<Image> portraits;

    float furlTime;
    float cellX;
    float spacing;

    void Start()
    {
        scroll = gameObject.transform;
        container = GetComponentInChildren<Transform>();
        FlexibleGridLayout grid = container.GetComponent<FlexibleGridLayout>();
        cellX = grid.cellWidth;
        spacing = grid.spacing.x;
    }

    public void StartTurnOrder(List<Character> turnOrder)
    {
        foreach(Character character in turnOrder)
        {
            GameObject temp = new GameObject();
            temp.transform.parent = container;
            Image temp2 = temp.AddComponent<Image>();
            temp2.sprite = character.GetCharacterData().Portrait;
            portraits.Add(temp2);
        }
        scroll.LeanScaleX((turnOrder.Count * (cellX + spacing)), furlTime);
        scroll.LeanMoveX(-(turnOrder.Count * (cellX + spacing)), furlTime);
    }

    public void UpdateTurnOrder(List<Character> turnOrder)
    {

    }
    public void StopDisplayingTurnOrder()
    {

    }
}
