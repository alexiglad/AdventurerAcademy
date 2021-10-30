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

        SetAP(7);
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
        Debug.Log(AP);
        coroutine = StartCoroutine(APCostPreview(AP));        
    }

    public void StopPreviewingAPCost()
    {
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
                ids.Add(groups[i].LeanAlpha(0, 1f).id);
            }
            yield return new WaitForSeconds(1f);
            for (int i = currentAP-1; i > currentAP - AP - 1; i--)
            {
                ids.Add(groups[i].LeanAlpha(1, 1f).id);
            }
            yield return new WaitForSeconds(1f);
        }
        #pragma warning disable CS0162 // Unreachable code detected
        yield return null;
        #pragma warning restore CS0162 // Unreachable code detected
    }
}
