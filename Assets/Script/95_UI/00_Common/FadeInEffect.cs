using System.Collections;
using UnityEngine;

public class FadeInEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.15f;
    private Coroutine fadeCoroutine;

    public void ShowWithFadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    public void HideWithFadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    public void SetAlpha(float Alphavalue)
    {
        canvasGroup.alpha = Alphavalue;
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        canvasGroup.alpha = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}