using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] List<Character> overlayCharacters;
    [SerializeField] List<ResourceBarUI> bars = new List<ResourceBarUI>();
    [SerializeField] List<Image> portraits = new List<Image>();
    [SerializeField] List<Transform> statusBars = new List<Transform>();
    [SerializeField] GameObject statusIconPrefab;

    void Start()
    {        
        UpdateOverlay();
    }

    void Update()
    {
        UpdateOverlay();
    }

    void OnDisable()
    {
        overlayCharacters.Clear();
        portraits.Clear();
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

        List<Image> temp = GetComponentsInChildren<Image>(true).ToList();
        if (portraits.Count <= 0)
        {
            for(int i = 0; i < temp.Count; i++)
            {
                if(temp[i].gameObject.name.Contains("CharacterPortrait"))
                {
                    portraits.Add(temp[i]);
                }
            }
        }

        for (int i = 0; i < portraits.Count && i < overlayCharacters.Count; i++)
        {
            portraits[i].sprite = overlayCharacters[i].GetCharacterData().Portrait;          
        }
        
        for (int i = 0; i < overlayCharacters.Count; i++)
        {
            if (portraits[i].gameObject.name.Contains("CharacterPortrait"))
            {
                portraits[i].gameObject.transform.parent.gameObject.SetActive(true);
            }
        }

        bars = GetComponentsInChildren<ResourceBarUI>(true).ToList();
        int index = 0;
        foreach (ResourceBarUI bar in bars)
        {
            //Every third bar            
            if (bar.transform.parent.parent.gameObject.activeSelf)
            {
                bar.SetCharacter(overlayCharacters[index / 2]);
                index += 1;
            }            
        }

        List<Transform> children = GetComponentsInChildren<Transform>(true).ToList();
        foreach(Transform child in children)
        {
            if(child.name == "StatusBar")
            {
                statusBars.Add(child);
            }
        }
    }

    public void AddStatuses(StatusData status, Sprite icon)
    {
        foreach (Character character in overlayCharacters)
        {
            if (character == status.Attackee)
            {
                int index = overlayCharacters.IndexOf(character);
                bool isDuplicate = false;
                GameObject duplicate = null; 
                foreach (Transform child in statusBars[index])
                {
                    if(child.name == icon.name)
                    {
                        isDuplicate = true;
                        duplicate = child.gameObject;
                    }
                }
                if (!isDuplicate)
                {
                    GameObject statusIcon = Instantiate(statusIconPrefab, statusBars[index]);
                    statusIcon.GetComponent<Image>().sprite = icon;
                    statusIcon.name = icon.name;
                }
                if (isDuplicate)
                {
                    UpdateStatusInfo(status, duplicate);
                }
            }
        }
    }

    void UpdateStatusInfo(StatusData status, GameObject statusIcon)
    {
        //Update pop-up info about status
    }

    public void RemoveStatus(StatusData status)
    {
        foreach(Character character in overlayCharacters)
        {
            if (character == status.Attackee)
            {
                Transform temp = null;
                int index = overlayCharacters.IndexOf(character);
                foreach (Transform child in statusBars[index])
                {
                    if (child.name == status.Status.StatusEffect.ToString())
                    {
                        temp = child;
                        break;
                    }
                }
                Destroy(temp?.gameObject);
            }
        }
    }
}