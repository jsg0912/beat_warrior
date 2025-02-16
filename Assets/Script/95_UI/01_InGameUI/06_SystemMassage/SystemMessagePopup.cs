using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessagePopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public float displayDuration = 3.0f;
    public TMP_Text messageText;
    public Image MapTitleImage;

    private Coroutine fadeCoroutine;
    private Coroutine hideCoroutine;

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

    public bool TurnOnPopup(bool isAffectedByDeltaTime = false, float DisplayDuration = 3.0f)
    {
        bool success = Util.SetActive(gameObject, true);
        if (success)
        {
            canvasGroup.alpha = 1;
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            if (hideCoroutine != null) StopCoroutine(hideCoroutine);
            hideCoroutine = StartCoroutine(AutoHideRoutine(isAffectedByDeltaTime, DisplayDuration));
        }
        return success;
    }

    public bool TurnOffPopup()
    {
        bool success = Util.SetActive(gameObject, false);
        return success;
    }

    private IEnumerator AutoHideRoutine(bool isAffectedByDeltaTime, float DisplayDuration)
    {
        if (isAffectedByDeltaTime == true) { yield return new WaitForSeconds(DisplayDuration); }
        else { yield return new WaitForSecondsRealtime(DisplayDuration); }
        FadeOut();
    }

    private IEnumerator FadeOutRoutine()
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        TurnOffPopup();
    }

    public void SetMessageText(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    public void SetBackgroundImage(Sprite sprite)
    {
        if (MapTitleImage != null)
        {
            MapTitleImage.sprite = sprite;
        }
    }
}