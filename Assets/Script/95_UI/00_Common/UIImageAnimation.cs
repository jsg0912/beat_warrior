using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImageAnimation : MonoBehaviour
{
    public Image uiImage; 
    public Sprite[] sprites; 
    public float frameRate = 0.1f;

    public FadeInEffect overlayFadeInEffect;
    public FadeInEffect fadeInEffect;

    public Image overlayImage;
    void Start()
    {
        overlayImage.gameObject.SetActive(false);
        StartCoroutine(AnimateSprite());
    }

    IEnumerator AnimateSprite()
    {
        yield return new WaitForSecondsRealtime(1f);
        SoundManager.Instance.SFXPlay("chapter2BossMapTitle", SoundList.Instance.chapter2BossMapTitle);
        for (int i = 0; i < sprites.Length; i++) 
        {
            uiImage.sprite = sprites[i];
            yield return new WaitForSeconds(frameRate);
        }
        overlayImage.gameObject.SetActive(true);
        overlayFadeInEffect.ShowWithFadeIn();
        yield return new WaitForSeconds(overlayFadeInEffect.fadeDuration);
        fadeInEffect.HideWithFadeOut();
        yield return new WaitForSeconds(2f);
        SystemMessageUIManager.Instance.isTimeLinePlaying = false;
    }
}
