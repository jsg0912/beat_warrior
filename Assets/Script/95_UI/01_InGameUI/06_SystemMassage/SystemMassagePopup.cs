using System.Collections;
using UnityEngine;

public class SystemMassagePopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public float displayDuration = 3.0f;

    private Coroutine fadeCoroutine;

    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOutRoutine());
    }

    public bool TurnOnPopup()
    {
        bool success = Util.SetActive(gameObject, true);
        if (success)
        {
            canvasGroup.alpha = 1;
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            StartCoroutine(AutoHideRoutine());
        }
        return success;
    }

    public bool TurnOffPopup()
    {
        bool success = Util.SetActive(gameObject, false);
        return success;
    }

    private IEnumerator AutoHideRoutine()
    {
        yield return new WaitForSeconds(displayDuration);
        FadeOut();
    }

    private IEnumerator FadeOutRoutine()
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        TurnOffPopup();
    }
}