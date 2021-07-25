using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderScroll : MonoBehaviour
{
    RectTransform scroll;
    Transform container;
    List<GameObject> portraits = new List<GameObject>();
    [SerializeField] protected GameStateManagerSO gameStateManager;


    [SerializeField] float furlTime;
    float cellX;
    float spacing;
    float defaultWidth;
    float defaultHeight;


    public void StartTurnOrder(List<Character> turnOrder)
    {
        scroll = (RectTransform)gameObject.transform;
        container = scroll.GetChild(0);
        FlexibleGridLayout grid = container.GetComponent<FlexibleGridLayout>();
        cellX = grid.cellSize.x;
        spacing = grid.spacing.x;
        container.gameObject.SetActive(true);
        defaultWidth = scroll.rect.width;
        defaultHeight = scroll.rect.height;
        StartCoroutine(Unfurl(turnOrder));
    }
    public void UpdateTurnOrder(List<Character> turnOrder)
    {
        StartCoroutine(UpdateTurnOrderCorutine(turnOrder));

    }
    public void StopDisplayingTurnOrder(List<Character> turnOrder)
    {
        StartCoroutine(StopDisplayingTurnOrderCorutine(turnOrder));
    }

    private IEnumerator StopDisplayingTurnOrderCorutine(List<Character> turnOrder)
    {
        yield return StartCoroutine(Furl(turnOrder, false));
    }
    private IEnumerator UpdateTurnOrderCorutine(List<Character> turnOrder)
    {
        yield return StartCoroutine(Furl(turnOrder, true));        
    }


    IEnumerator Furl(List<Character> turnOrder, bool ctx)
    {        
        portraits.Reverse();

        foreach (GameObject element in portraits)
        {
            element.GetComponent<CanvasGroup>().LeanAlpha(0, furlTime / 2);            
        }

        yield return new WaitForSeconds(furlTime/3);

        foreach (GameObject element in portraits)
        {
            Destroy(element);
        }

        scroll.LeanSize(new Vector2(defaultWidth, defaultHeight), furlTime);

        portraits.Clear();

        yield return new WaitForSeconds(furlTime);

        if (ctx)
        {
            StartCoroutine(Unfurl(turnOrder));
        }
        else
        {
            //cedric here
        }
    }

    IEnumerator Unfurl(List<Character> turnOrder)
    {
        scroll.LeanSize(new Vector2(defaultWidth + (turnOrder.Count * (cellX + spacing)), defaultHeight), furlTime);
        foreach (Character character in turnOrder)
        {
            yield return new WaitForSeconds(furlTime / turnOrder.Count);
            GameObject obj = new GameObject();
            obj.transform.parent = container;
            Image image = obj.AddComponent<Image>();
            image.sprite = character.GetCharacterData().Portrait;
            CanvasGroup canvas = obj.AddComponent<CanvasGroup>();
            canvas.alpha = 0;
            canvas.LeanAlpha(1, .5f);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "Scroll Portrait " + portraits.Count;
            portraits.Add(obj);
        }
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.EnableCombatInput();
        }
    }
}
