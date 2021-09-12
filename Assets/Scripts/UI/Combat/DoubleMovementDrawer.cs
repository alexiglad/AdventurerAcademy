using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class DoubleMovementDrawer : MonoBehaviour
{
    [SerializeField] GameStateManagerSO gameStateManager;
    Image image;
    List<Sprite> frames;
    float frameRate;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }
    void OnEnable()
    {
        if(gameStateManager.GetCurrentGameState() == GameStateEnum.Combat && gameStateManager.GetCurrentGameStateManager() != null)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            Character tempChar = tempRef.Character;
            if (tempChar.IsPlayer())
            {
                frames = tempChar.GetCharacterData().DoubleMovmenetAnimationFrames;
                frameRate = tempChar.GetCharacterData().FrameRate;
            }
            StartCoroutine(Play());
        }
    }

    IEnumerator Play()
    {
        while (gameObject.activeSelf)
        {
            foreach (Sprite sprite in frames)
            {
                image.sprite = sprite;
                yield return new WaitForSeconds(frameRate);
            }
        }
        yield return null;
    }
}
