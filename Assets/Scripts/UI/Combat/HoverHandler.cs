using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class HoverHandler : MonoBehaviour
{
    [SerializeField] InputHandler controls;

    void Update()
    {
        RaycastData data = controls.GetRaycastHit();
        if (data.HitBool && controls.VerifyTag(data, "Character"))
        {
            DisplayCharacterHover(data.Hit.collider.GetComponent<Character>());
        }
    }

    public void DisplayAbilityHover(object sender, AbilityEventArgs e)
    {
        if (e.NewAbility != null && !e.Selected)
        {
            //Debug.Log("Hovering Over: " + e.NewAbility.name + "Button");
        }
    }

    void DisplayCharacterHover(Character character)
    {
        if(character != null)
        {
            //Debug.Log("Hovering Over: " + character.GetName());
        }
    }
}