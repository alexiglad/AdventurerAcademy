using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AbilityImageDrawer : MonoBehaviour
{
    [SerializeField] float backgroundTargatAlpha;
    [SerializeField] float fadeInTime;
    [SerializeField] float imageAnimationTime;

    bool slideRight = true; //if false, slides up from the bottom

    Image abilityImage;
    Transform abilityTransform;
    CanvasGroup abilityCanvas;
    CanvasGroup backgroundCanvas;

    void Start()
    {
        List<Transform> children = gameObject.GetComponentsInChildren<Transform>().ToList();
        foreach (Transform child in children)
        {
            if(child.gameObject.name.Equals("AbilityImage"))
            {
                abilityImage = child.GetComponent<Image>();
                abilityTransform = child;
            }
            if(child.gameObject.name.Equals("AbilityImage"))
            {
                abilityCanvas = gameObject.GetComponent<CanvasGroup>();
            }
            if (child.gameObject.name.Equals("Background"))
            {
                backgroundCanvas = gameObject.GetComponent<CanvasGroup>();
            }
        }
    }
    void OnEnable()
    {
        backgroundCanvas.alpha = 0;
        abilityCanvas.alpha = 0;
        backgroundCanvas.LeanAlpha(backgroundTargatAlpha, fadeInTime);
        abilityCanvas.LeanAlpha(1, fadeInTime);

        if (slideRight)
        {
            abilityTransform.localPosition = new Vector2(-100, 0);
            abilityTransform.LeanMoveLocalX(0, imageAnimationTime);
        }            
        else
        {
            abilityTransform.localPosition = new Vector2(0, -100);
            abilityTransform.LeanMoveLocalY(0, imageAnimationTime);
        }

        StartCoroutine(Deactivate());
    }
    
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(imageAnimationTime);
        gameObject.SetActive(false);
    }
    
    public void SetSprite(Sprite sprite)
    {
        abilityImage.sprite = sprite;
    }
}
