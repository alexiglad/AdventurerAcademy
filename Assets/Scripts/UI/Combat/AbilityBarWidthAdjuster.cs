using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarWidthAdjuster : MonoBehaviour
{
    RectTransform rt;
    FlexibleGridLayout grid;
    [SerializeField] GameStateManagerSO gameStateManager;
    CombatManager combatManager;
    [SerializeField] List<Image> allImages;
    float defaultWidth;
    int defaultChildCount;
    float defaultCellWidth;

    public void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        grid = gameObject.GetComponent<FlexibleGridLayout>();
        allImages = GetComponentsInChildren<Image>(true).ToList();
        defaultWidth = grid.parentWidth;
        defaultChildCount = gameObject.transform.childCount; //max number of abilities on bar (currently 5).
        defaultCellWidth = grid.cellWidth;
    }
    void Update()
    {
        float activeChildren = ActiveChildren();
        float v = ((defaultChildCount - activeChildren) * defaultCellWidth);
        rt.sizeDelta = new Vector2(defaultWidth - v, grid.parentHeight);
        if (combatManager == null && gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
            combatManager = (CombatManager)gameStateManager.GetCurrentGameStateManager();
        for(int i = 1; i < allImages.Count && i <= activeChildren; i++)
        {
            allImages[i].sprite = combatManager.Character.GetCharacterData().InUseAbilities[i-1].Icon;
        }
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