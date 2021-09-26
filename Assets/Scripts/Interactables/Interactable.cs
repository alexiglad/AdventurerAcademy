using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region local variables
    [SerializeField] private Interaction[] interactions;//array of all interactions in a given Game Object
    private Interaction interaction;
    private IEnumerator enumerator;



    #endregion
    //every interactable needs a navmeshobstacle, (maybe an agent), (maybe an animator), boxcollider, rigidbody, sprite renderer 
    private void OnEnable()
    {
        enumerator = interactions.GetEnumerator();
        enumerator.MoveNext();
        interaction = (Interaction)enumerator.Current;

    }



    public void IterateIteractions()
    {
        if (enumerator.MoveNext())//TODO add system to determine whether enumerator should continue automatically or not
        {
            interaction = (Interaction)enumerator.Current;
            Debug.Log("iterated through interactions");
        }
        else
        {
            Debug.Log("no more interactions will repeat current");
        }
    }
    public void HandleInteraction()
    {
        interaction.HandleInteraction();

    }

}
