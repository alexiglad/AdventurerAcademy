using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class APBarHandler : MonoBehaviour
{
    List<Image> ap = new List<Image>();
    List<CanvasGroup> groups = new List<CanvasGroup>();
    Coroutine coroutine;
    int currentAP;
    int previewAP;
    List<int> ids = new List<int>();

    void Start()
    {
        ap = gameObject.GetComponentsInChildren<Image>(true).ToList(); 
        groups = gameObject.GetComponentsInChildren<CanvasGroup>(true).ToList();
        groups.RemoveAt(0);

        foreach (Image image in ap)
        {
            image.color = Color.gray;
        }
        gameObject.GetComponent<CanvasGroup>().LeanAlpha(1f, .5f);
    }

    public void SetAP(int AP)
    {
        currentAP = AP;
        for(int i = 0; i < AP; i++)
        {
            ap[i].color = Color.green;
        }

        for(int i = AP; i < ap.Count; i++)
        {
            ap[i].color = Color.grey;
        }
    }

    public void PreviewAPCost(int AP)
    {
        if(AP != previewAP)
        {
            StopPreviewingAPCost();
            coroutine = StartCoroutine(APCostPreview(AP));        
        }
        previewAP = AP;
    }

    public void StopPreviewingAPCost()
    {
        previewAP = -1;
        if (coroutine == null)
        {
            return;
        }
        StopCoroutine(coroutine);
        for(int i = 0; i < ids.Count; i++)
        {
            LeanTween.cancel(ids[i]);
        }
        ids.Clear();
        foreach (CanvasGroup group in groups)
        {
            group.alpha = 1f;
        }
    }

    IEnumerator APCostPreview (int AP)
    {
        while (true)
        {
            for(int i = currentAP-1; i > currentAP - AP - 1; i--)
            {
                ids.Add(groups[i].LeanAlpha(0, .5f).id);
            }
            yield return new WaitForSeconds(.5f);
            for (int i = currentAP-1; i > currentAP - AP - 1; i--)
            {
                ids.Add(groups[i].LeanAlpha(1, .5f).id);
            }
            yield return new WaitForSeconds(.5f);
        }
        #pragma warning disable CS0162 // Unreachable code detected
        yield return null;
        #pragma warning restore CS0162 // Unreachable code detected TODO cedric do we 
    }
}
