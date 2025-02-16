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
        //StartCoroutine(AnimateSprite());
    }

    public void Initialize()
    {
        if(sprites.Length>0)
        {
            uiImage.sprite = sprites[0];
        }

        overlayImage.gameObject.SetActive(false);
    }

    public IEnumerator AnimateSprite()
    {
        fadeInEffect.SetAlpha(1f);
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
