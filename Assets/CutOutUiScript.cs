using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOutUiScript : MonoBehaviour
{
    public bool isFadeOut = true;
    private RectTransform _rect;
    private Animator _animator;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
        
        if (isFadeOut)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        _rect.sizeDelta = new Vector2(5000, 5000);
        _animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _rect.sizeDelta = new Vector2(0, 0);
        _animator.SetTrigger("FadeOut");
    }
}
