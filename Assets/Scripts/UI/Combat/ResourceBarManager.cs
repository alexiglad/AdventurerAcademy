using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarManager : MonoBehaviour
{
    [SerializeField] List<ResourceBarUI> bars;

    void OnEnable()
    {
        List<ResourceBarUI>  bars = GetComponentsInChildren<ResourceBarUI>().ToList();
    }

    public void UpdateHealth()
    {

    }
}
