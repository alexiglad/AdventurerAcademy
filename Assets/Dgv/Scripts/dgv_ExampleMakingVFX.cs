using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dgv_ExampleMakingVFX : MonoBehaviour
{
    [Header("Grab desired material")]
    [SerializeField]
    GameObject _spriteGO;
    [Space]
    SpriteRenderer _spriteGoRenderer;

    void Start()
    {
        _spriteGoRenderer = _spriteGO.GetComponent<SpriteRenderer>();

        StartCoroutine(_DoVfx());
    }


    public IEnumerator _DoVfx() {
        yield return new WaitForSeconds(2f);
        _spriteGoRenderer.material.SetFloat("_grayOut",1);
    }
}
