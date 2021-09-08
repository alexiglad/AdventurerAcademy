using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] GameLoader gameLoader;
    private void Start()
    {
        gameLoader.ManualStart();
    }

}
