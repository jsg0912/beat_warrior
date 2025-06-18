using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public bool isPlaying { get; private set; } = false;
    private bool isFading = false; // TODO: 버그가 생길 수 있으니 이를 활용한 로직이 많아지면 개선 필요 - SDH, 20250618
    public CutSceneKind cutSceneKind;
    [SerializeField] private TMP_Text cutSceneText;
    [SerializeField] private List<FadeInOutImage> cutSceneObjects;
    [SerializeField] private Queue<string> dialogQueue = new Queue<string>();

    public FadeInEffect fadeInEffect;

    private int currentIndex = 0;

    public void TutorialCommandCheck()
    {
        if (isFading) return;
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
        {
            NextCutScene();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndCutScene();
        }
    }

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
        if (dialogQueue.Count > 0)
        {
            cutSceneText.text = dialogQueue.Dequeue();
            return;
        }

        if (currentIndex < cutSceneObjects.Count - 1)
        {
            isFading = true;
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
        cutSceneObjects[currentIndex].ShowWithFadeIn(() => { isFading = false; });
        string[] dialogs = DialogScript.CutSceneData[cutSceneKind][GameManager.Instance.Language][currentIndex].Split('\r');
        foreach (string dialog in dialogs)
        {
            dialogQueue.Enqueue(dialog);
        }
        cutSceneText.text = dialogQueue.Count > 0 ? dialogQueue.Dequeue() : string.Empty;
    }

    public void EndCutScene()
    {
        if (cutSceneObjects.Count > 0)
        {
            isFading = true;
            cutSceneObjects[currentIndex].HideWithFadeOut(() =>
            {
                currentIndex = 0;
                cutSceneText.text = string.Empty;
                isPlaying = false;
                isFading = false;
                fadeInEffect.HideWithFadeOut();
                UIManager.Instance.SetActiveMiniMap(true);
            });
        }

    }
}
