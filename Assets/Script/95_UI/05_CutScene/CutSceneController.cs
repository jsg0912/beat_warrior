using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public bool isPlaying { get; private set; } = false;
    public CutSceneKind cutSceneKind;
    [SerializeField] private TMP_Text cutSceneText;
    [SerializeField] private List<FadeInOutImage> cutSceneObjects;

    public FadeInEffect fadeInEffect;

    private int currentIndex = 0;

    public void StartCutScene()
    {
        UIManager.Instance.SetActiveMiniMap(false);
        fadeInEffect.ShowWithFadeIn();
        if (cutSceneObjects.Count > 0)
        {
            isPlaying = true;
            ShowCutScene();
        }
    }

    public void NextCutScene()
    {
        if (currentIndex < cutSceneObjects.Count - 1)
        {
            cutSceneObjects[currentIndex++].HideWithFadeOut(() =>
            {
                ShowCutScene();
            });
        }
        else
        {
            EndCutScene();
        }
    }

    public void ShowCutScene()
    {
        cutSceneObjects[currentIndex].ShowWithFadeIn();
        cutSceneText.text = DialogScript.CutSceneData[cutSceneKind][GameManager.Instance.Language][currentIndex];
    }

    public void EndCutScene()
    {
        if (cutSceneObjects.Count > 0)
        {
            cutSceneObjects[currentIndex].HideWithFadeOut(() =>
            {
                currentIndex = 0;
                cutSceneText.text = string.Empty;
                isPlaying = false;
                fadeInEffect.HideWithFadeOut();
                UIManager.Instance.SetActiveMiniMap(true);
            });
        }
        
    }
}
