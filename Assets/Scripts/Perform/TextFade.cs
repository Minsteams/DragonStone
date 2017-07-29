using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 被控制地加于文字并控制淡入淡出效果
/// </summary>
[ExecuteInEditMode]
[AddComponentMenu("演出/TextFade")]
public class TextFade : MonoBehaviour
{
    float fadeSeconds = 0.4f;
    bool isFading = false;
    bool ifFadeIn = true;
    bool isOver = false;
    float time = 0;
    float originalAlpha = 1;
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
            GetComponent<Text>().color = c;
            if (isOver) Destroy(this);
        }
    }
    public void FadeIn(float seconds)
    {
        fadeSeconds = seconds;
        FadeInit(true);
        c.a = 0;
        GetComponent<Text>().color = c;
    }
    public void FadeOut(float seconds)
    {
        fadeSeconds = seconds;
        FadeInit(false);
    }
    void FadeInit(bool ifIn)
    {
        ifFadeIn = ifIn;
        c = GetComponent<Text>().color;
        originalAlpha = c.a;
        time = 0;
        isFading = true;
    }
    [ContextMenu("Test")]
    void FadeTest()
    {
        FadeOut(2);
    }
}
