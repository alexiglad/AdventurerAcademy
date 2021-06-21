using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBarWidthAdjuster : MonoBehaviour
{
    RectTransform rt;
    FlexibleGridLayout grid;
    float defaultWidth;
    int defaultChildCount;
    float defaultCellWidth;

    public void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        grid = gameObject.GetComponent<FlexibleGridLayout>();
        defaultWidth = grid.parentWidth;
        defaultChildCount = gameObject.transform.childCount; //max number of abilities on bar (currently 5).
        defaultCellWidth = grid.cellWidth;
    }
    void Update()
    {
        float v = ((defaultChildCount - ActiveChildren()) * defaultCellWidth);
        rt.sizeDelta = new Vector2(defaultWidth - v, grid.parentHeight);
    }

    int ActiveChildren()
    {
        int returnable = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                returnable++;
        }
        return returnable;
    }
}