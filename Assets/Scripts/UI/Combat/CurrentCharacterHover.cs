using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CurrentCharacterHover : MonoBehaviour
{
    Character characterToFollow;
    readonly float followHeight = .25f;
    public void SetCharacterToFollow(Character character)
    {
        characterToFollow = character;
        SetEnabled(true);
    }
    public void SetEnabled(bool enable)
    {
        gameObject.SetActive(enable);
    }
    private void Update()
    {
        if(characterToFollow != null && gameObject.activeInHierarchy)
        {
            //follow character
            Vector3 characterTop = characterToFollow.CharacterTop();
            characterTop.y += followHeight;
            gameObject.transform.position = characterTop;
        }
    }
}
