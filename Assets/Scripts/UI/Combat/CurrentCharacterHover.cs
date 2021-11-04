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
        SetEnabled(true);
        StartCoroutine(MakeReappear(character));

    }
    IEnumerator MakeReappear(Character character)
    {
        gameObject.LeanAlpha(0, .25f);
        yield return new WaitForSeconds(0.25f);
        characterToFollow = character;
        gameObject.LeanAlpha(.5f, .75f);
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
