using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FollowUpAnimationDrawer : MonoBehaviour
{
    [SerializeField] float backgroundTargatAlpha;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    
    [SerializeField] protected GameStateManagerSO gameStateManager;

    Image followUpImage;
    List<Sprite> anim = new List<Sprite>();
    RectTransform followUpTransform;
    CanvasGroup followUpCanvas;
    CanvasGroup backgroundCanvas;
    Vector2 targetPosition;
    float imageAnimationTime;
    float framerate;

    void Start()
    {
        List<Transform> children = gameObject.GetComponentsInChildren<Transform>().ToList();
        foreach (Transform child in children)
        {
            if (child.gameObject.name.Equals("FollowUpAnimation"))
            {
                followUpImage = child.GetComponent<Image>();
                followUpTransform = child.GetComponent<RectTransform>();
                followUpCanvas = child.GetComponent<CanvasGroup>();
            }
            if (child.gameObject.name.Equals("Background"))
            {
                backgroundCanvas = child.GetComponent<CanvasGroup>();
            }
        }
    }
    public void DisplayFollowUp(FollowUpData followUp)
    {
        SetAnimationSprites(followUp.FollowUp.Sprites);
        SetScale(followUp.FollowUp.ScaleX, followUp.FollowUp.ScaleY);
        SetPosition(followUp.FollowUp.PosX, followUp.FollowUp.PosY);
        SetLength(followUp.FollowUp.GetAnimationLength());
        SetFrameRate(followUp.FollowUp.FrameRate);
        PlayAnimation();
    }
    IEnumerator Play()
    {
        foreach (Sprite sprite in anim)
        {
            followUpImage.sprite = sprite;
            yield return new WaitForSeconds(framerate);
        }
        StartCoroutine(Deactivate());
    }

    public void PlayAnimation()
    {
        backgroundCanvas.alpha = 0;
        followUpCanvas.alpha = 0;
        backgroundCanvas.blocksRaycasts = true;
        backgroundCanvas.LeanAlpha(backgroundTargatAlpha, fadeInTime);
        followUpCanvas.LeanAlpha(1, fadeInTime);

        StartCoroutine(Play());
        
    }

    IEnumerator Deactivate()
    {
        backgroundCanvas.LeanAlpha(0, fadeOutTime);
        followUpCanvas.LeanAlpha(0, fadeOutTime);
        yield return new WaitForSeconds(fadeOutTime);
        if (gameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            tempRef.EnableCombatInput();
        }
        backgroundCanvas.blocksRaycasts = false;
    }

    public void SetAnimationSprites(List<Sprite> sprites)
    {
        anim = sprites;
    }

    public void SetScale(float scaleX, float scaleY)
    {
        followUpTransform.localScale = new Vector2(scaleX, scaleY);
    }

    public void SetPosition(float x, float y)
    {
        followUpTransform.localPosition = new Vector2(x, y);
    }

    public void SetLength(float length)
    {
        imageAnimationTime = length;
    }

    public void SetFrameRate(float rate)
    {
        framerate = rate;
    }
}
