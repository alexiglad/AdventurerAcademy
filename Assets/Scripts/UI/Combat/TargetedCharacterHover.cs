using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TargetedCharacterHover : MonoBehaviour
{
    Character characterToFollow;
    public void SetCharacterToFollow(Character character)
    {
        Vector3 characterBottom = character.CharacterBottom();
        gameObject.transform.position = characterBottom;
        BoxCollider box = gameObject.GetComponentInChildren<BoxCollider>();
        float z = box.size.y;
        box.size = new Vector3(character.Agent.radius, character.Agent.radius, z);
        SetEnabled(true);
    }
    public void SetEnabled(bool enable)
    {
        gameObject.SetActive(enable);
    }
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            //flash in/out

        }
    }

}
