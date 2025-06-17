using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public bool isPlaying { get; private set; } = false;
    public CutSceneKind cutSceneKind;
    [SerializeField] private TMP_Text cutSceneText;
    [SerializeField] private List<FadeInOutImage> cutSceneObjects;
    [SerializeField] private Queue<string> dialogQueue = new Queue<string>();

    public FadeInEffect fadeInEffect;

    private int currentIndex = 0;

    public void TutorialCommandCheck()
    {
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
