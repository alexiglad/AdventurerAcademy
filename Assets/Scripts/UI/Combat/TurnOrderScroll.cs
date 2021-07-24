using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderScroll : MonoBehaviour
{
    RectTransform scroll;
    Transform container;
    List<GameObject> portraits = new List<GameObject>();

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
        StartCoroutine(Furl(turnOrder, false));
        container.gameObject.SetActive(false);
    }
    private IEnumerator UpdateTurnOrderCorutine(List<Character> turnOrder)
    {
        yield return StartCoroutine(Furl(turnOrder, true));        
    }


    IEnumerator Furl(List<Character> turnOrder, bool ctx)
    {
        Debug.Log("Furling...");
        scroll.LeanSize(new Vector2(defaultWidth, defaultHeight), furlTime);
        portraits.Reverse();

        foreach (GameObject element in portraits)
        {            
            element.GetComponent<CanvasGroup>().LeanAlpha(0, .03f);
            yield return new WaitForSeconds(furlTime / turnOrder.Count);            
        }

        foreach(GameObject element in portraits)
            Destroy(element);

        portraits.Clear();

        if (ctx)
            StartCoroutine(Unfurl(turnOrder));
    }

    IEnumerator Unfurl(List<Character> turnOrder)
    {
        Debug.Log("Unfurling...");
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
            portraits.Add(obj);
        }
    }
}
