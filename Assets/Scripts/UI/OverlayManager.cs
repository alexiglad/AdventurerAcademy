using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    [SerializeField] GameStateManagerSO gameStateManager;
    List<Character> overlayCharacters;
    List<ResourceBarUI> bars = new List<ResourceBarUI>();

    void Start()
    {        
        UpdateOverlay();
    }

    void Update()
    {
        UpdateOverlay();
    }

    public void UpdateOverlay()
    {
        switch (gameStateManager.GetCurrentGameStateManager().GetType().Name)
        {
            case nameof(CombatManager):
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                overlayCharacters = tempRef.UserCharacters.ToList();
                break;
        }//Can add more cases if we want to use the overlay in other managers besides combat

        if (bars.Count <= 0)
            bars = transform.GetComponentsInChildren<ResourceBarUI>().ToList();

        float index = 0;
        foreach (ResourceBarUI bar in bars)
        {
            bar.SetCharacter(overlayCharacters[(int)index]);
            index += 0.5f;
        }
    }
}
