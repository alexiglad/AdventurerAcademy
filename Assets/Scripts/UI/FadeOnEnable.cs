using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnEnable : MonoBehaviour
{
    CanvasGroup canvas;
    [SerializeField] float fadeTime;
    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.LeanAlpha(1, fadeTime);
    }
    private void OnEnable()
    {        
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        canvas.LeanAlpha(1, fadeTime);
    }
}
