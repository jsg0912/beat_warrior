using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutImage : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 0.15f;

    public void ShowWithFadeIn(Action callback = null)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        StartCoroutine(FadeIn(callback));
    }

    public void HideWithFadeOut(Action callback = null)
    {
        StartCoroutine(FadeOut(callback));
    }

    private IEnumerator FadeIn(Action callback)
    {
        float time = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0f, 1f, time / fadeDuration));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        callback?.Invoke();
    }

    private IEnumerator FadeOut(Action callback)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1f, 0f, time / fadeDuration));
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        gameObject.SetActive(false);
        callback?.Invoke();
    }
}