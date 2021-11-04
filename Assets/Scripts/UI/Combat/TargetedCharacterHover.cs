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
        

    }
    public void StopFollowing()
    {
        characterToFollow = null;
    }
    private void Update()
    {
        if (characterToFollow != null)
        {
            //flash in/out
        }
    }

}
