using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class HoverHandler : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    TextMeshProUGUI characterName;
    TextMeshProUGUI healthDisplay;
    GameObject characterHoverObject;

    void OnEnable()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true).ToList())
        {
            if(child.name == "HealthText")
            {
                healthDisplay = child.GetComponent<TextMeshProUGUI>();
            }

            if (child.name == "CharacterName")
            {
                characterName = child.GetComponent<TextMeshProUGUI>();
            }

            if (child.name == "CharacterHoverPopUp")
            {
                characterHoverObject = child.gameObject;
            }

        }
    }

    void Update()
    {
        RaycastData data = controls.GetRaycastHit();
        if (data.HitBool && controls.VerifyTag(data, "Character"))
        {
            DisplayCharacterHoverOverlay(data.Hit.collider.GetComponent<Character>());
        }
        else
        {
            DisableCharacterHoverOverlay();
        }
    }

    public void DisplayAbilityHover(object sender, AbilityEventArgs e)
    {
        if (e.NewAbility != null && !e.Selected)
        {
            //Debug.Log("Hovering Over: " + e.NewAbility.name + "Button");
        }
    }

    void DisplayCharacterHoverOverlay(Character character)
    {
        if(character != null)
        {
            healthDisplay.SetText(character.GetHealth() +"/" +character.GetMaxHealth());
            characterName.SetText(character.GetName());
            characterHoverObject.SetActive(true);
        }
    }

    void DisableCharacterHoverOverlay()
    {
        characterHoverObject.SetActive(false);
    }
}