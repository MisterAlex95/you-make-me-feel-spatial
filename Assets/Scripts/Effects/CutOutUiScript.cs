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
        GetComponent<RectTransform>().sizeDelta = new Vector2(5000, 5000);
        GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
