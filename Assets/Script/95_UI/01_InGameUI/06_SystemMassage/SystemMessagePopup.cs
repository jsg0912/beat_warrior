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

    private Coroutine hideCoroutine;

    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void TurnOnPopup(float displayDuration = 2.0f, bool isStayWhenPause = false)
    {
        Util.SetActive(gameObject, true);
        TryStopHideCoroutine();
        hideCoroutine = StartCoroutine(AutoHideRoutine(displayDuration, isStayWhenPause));
    }

    private void TryStopHideCoroutine()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
            canvasGroup.alpha = 1.0f;
        }
    }

    public bool TurnOffPopup()
    {
        bool success = Util.SetActive(gameObject, false);
        return success;
    }

    private IEnumerator AutoHideRoutine(float displayDuration, bool isStayWhenPause)
    {
        if (isStayWhenPause == true) { yield return new WaitForSeconds(displayDuration); }
        else { yield return new WaitForSecondsRealtime(displayDuration); }
        yield return FadeOutRoutine();
        TurnOffPopup();
    }

    // TODO: Util이나 FadeOutController 만들기
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