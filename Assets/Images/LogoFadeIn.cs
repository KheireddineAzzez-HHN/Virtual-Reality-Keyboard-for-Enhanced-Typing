using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class LogoFadeIn : MonoBehaviour
{
    public CanvasGroup logoCanvasGroup;
    public float fadeDuration = 1.0f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            logoCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
        logoCanvasGroup.alpha = 1;
    }
}
