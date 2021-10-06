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
    TextMeshProUGUI healthText;
    ResourceBarUI healthBarBack;
    ResourceBarUI healthBarFill;
    GameObject characterHoverObject;

    void OnEnable()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true).ToList())
        {
            if(child.name == "HealthText")
            {
                healthText = child.GetComponent<TextMeshProUGUI>();
            }

            if (child.name == "CharacterName")
            {
                characterName = child.GetComponent<TextMeshProUGUI>();
            }

            if (child.name == "CharacterHoverPopUp")
            {
                characterHoverObject = child.gameObject;
            }

            if (child.name == "HealthBarBack")
            {
                healthBarBack = child.GetComponent<ResourceBarUI>();
            }

            if (child.name == "HealthBarFill")
            {
                healthBarFill = child.GetComponent<ResourceBarUI>();
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
            healthText.SetText(character.GetHealth() +"/" +character.GetMaxHealth());
            characterName.SetText(character.GetName());
            healthBarBack.MaxValue = character.GetMaxHealth();
            healthBarBack.CurrentValue = character.GetHealth();
            healthBarBack.TargetCharacter = character;
            healthBarFill.MaxValue = character.GetMaxHealth();
            healthBarFill.CurrentValue = character.GetHealth();
            healthBarFill.TargetCharacter = character;
            healthBarBack.UpdateValues(null, new CharacterDamagedArgs(character));
            healthBarFill.UpdateValues(null, new CharacterDamagedArgs(character));
            characterHoverObject.SetActive(true);
        }
    }

    void DisableCharacterHoverOverlay()
    {
        characterHoverObject.SetActive(false);
    }
}