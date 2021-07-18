using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AbilityImageDrawer : MonoBehaviour
{
    [SerializeField] float backgroundTargatAlpha;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    [SerializeField] float imageAnimationTime;
    [SerializeField] protected GameStateManagerSO gameStateManager;


    Image abilityImage;
    RectTransform abilityTransform;
    CanvasGroup abilityCanvas;
    CanvasGroup backgroundCanvas;
    AbilityImageTweenEnum direction;

    void Start()
    {
        List<Transform> children = gameObject.GetComponentsInChildren<Transform>().ToList();
        foreach (Transform child in children)
        {
            if(child.gameObject.name.Equals("AbilityImage"))
            {
                abilityImage = child.GetComponent<Image>();
                abilityTransform = child.GetComponent<RectTransform>();
            }
            if(child.gameObject.name.Equals("AbilityImage"))
            {
                abilityCanvas = child.GetComponent<CanvasGroup>();
            }
            if (child.gameObject.name.Equals("Background"))
            {
                backgroundCanvas = child.GetComponent<CanvasGroup>();
            }
        }
    }

    public bool PlayAnimation()
    {
        backgroundCanvas.alpha = 0;
        abilityCanvas.alpha = 0;
        backgroundCanvas.LeanAlpha(backgroundTargatAlpha, fadeInTime);
        abilityCanvas.LeanAlpha(1, fadeInTime);

        switch (direction)
        {
            case AbilityImageTweenEnum.Up:
                abilityTransform.localPosition = new Vector2(0, -100);
                abilityTransform.LeanMoveLocalY(0, imageAnimationTime);
                break;
            case AbilityImageTweenEnum.Down:
                abilityTransform.localPosition = new Vector2(0, 100);
                abilityTransform.LeanMoveLocalY(0, imageAnimationTime);
                break;
            case AbilityImageTweenEnum.Left:
                abilityTransform.localPosition = new Vector2(100, 0);
                abilityTransform.LeanMoveLocalX(0, imageAnimationTime);
                break;
            case AbilityImageTweenEnum.Right:
                abilityTransform.localPosition = new Vector2(-100, 0);
                abilityTransform.LeanMoveLocalX(0, imageAnimationTime);
                break;
        }

        StartCoroutine(Deactivate());
        return true;
    }
    
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(imageAnimationTime);
        backgroundCanvas.LeanAlpha(0, fadeOutTime);
        abilityCanvas.LeanAlpha(0, fadeOutTime);
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.CanContinue = true;
        }

    }
    
    public void SetSprite(Sprite sprite)
    {
        abilityImage.sprite = sprite;
    }

    public void SetDirection(AbilityImageTweenEnum value)
    {
        direction = value;
    }

    public void SetDimensions(float width, float height)
    {
        abilityTransform.sizeDelta = new Vector2(width, height);
    }
}
