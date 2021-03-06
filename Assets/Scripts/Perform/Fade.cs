﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 被控制地加于任何物体上并控制淡入淡出效果
/// </summary>
[ExecuteInEditMode]
[AddComponentMenu("演出/Fade")]
public class Fade : MonoBehaviour
{
    float fadeSeconds = 0.4f;
    bool isFading = false;
    bool ifFadeIn = true;
    bool isOver = false;
    float time = 0;
    float originalAlpha = 1;
    string colorName = "_Color";
    Color c;

    void Update()
    {
        if (isFading)
        {
            time += Time.deltaTime;
            float bili = time / fadeSeconds;
            if (bili > 1)
            {
                bili = 1;
                isOver = true;
            }
            c.a = originalAlpha * (ifFadeIn ? bili : 1 - bili);
            GetComponent<Renderer>().material.SetColor(colorName, c);

            if (isOver) Destroy(this);
        }
    }
    public void FadeIn(float seconds, float a = 0)
    {
        fadeSeconds = seconds;
        FadeInit(true, a);
        c.a = 0;
        GetComponent<Renderer>().material.SetColor(colorName, c);
    }
    public void FadeOut(float seconds)
    {
        fadeSeconds = seconds;
        FadeInit(false);
    }
    void FadeInit(bool ifIn, float a = 0)
    {
        ifFadeIn = ifIn;
        c = GetComponent<Renderer>().material.GetColor(colorName);
        originalAlpha = a > 0.01f ? a : (c.a > 0.01f ? c.a : 1);
        time = 0;
        isFading = true;
    }
}
