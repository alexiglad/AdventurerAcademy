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
    Vector2 targetPosition;

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
        backgroundCanvas.blocksRaycasts = true;
        backgroundCanvas.LeanAlpha(backgroundTargatAlpha, fadeInTime);
        abilityCanvas.LeanAlpha(1, fadeInTime);

        switch (direction)
        {
            case AbilityImageTweenEnum.Up:
                abilityTransform.LeanMoveLocal(targetPosition, imageAnimationTime);
                break;
            case AbilityImageTweenEnum.Down:
                abilityTransform.LeanMoveLocal(targetPosition, imageAnimationTime);
                break;
            case AbilityImageTweenEnum.Left:
                abilityTransform.LeanMoveLocal(targetPosition, imageAnimationTime);
                break;
            case AbilityImageTweenEnum.Right:
                abilityTransform.LeanMoveLocal(targetPosition, imageAnimationTime);
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
        yield return new WaitForSeconds(fadeOutTime);
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.EnableCombatInput();
        }
        backgroundCanvas.blocksRaycasts = false;
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

    public void SetStartingPosition(float x, float y)
    {
        abilityTransform.localPosition = new Vector2(x, y);
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPosition = new Vector2(x, y);
    }
}