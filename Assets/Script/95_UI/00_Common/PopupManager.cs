using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    public void Awake() { Instance = this; }

    private List<PopupSystem> popupSystemStack = new();

    private bool IsAnyPopupAlive()
    {
        return popupSystemStack.Count > 0;
    }

    private bool PopPopup()
    {
        bool success = popupSystemStack[popupSystemStack.Count - 1].TurnOffPopup();
        if (success)
        {
            popupSystemStack.RemoveAt(popupSystemStack.Count - 1);
            TryResumeGame();
        }
        return success;
    }

    public bool TryPopPopup()
    {
        bool success = false;
        if (IsAnyPopupAlive())
        {
            success = PopPopup();
        }
        return success;
    }

    public void PushPopup(PopupSystem popupSystem)
    {
        popupSystemStack.Add(popupSystem);
        if (GameManager.Instance.isInGame)
            PauseController.instance.TryPauseGame();
    }

    public bool RemovePopup(PopupSystem popupSystem)
    {
        bool success = popupSystemStack.Remove(popupSystem);
        if (success)
        {
            TryResumeGame();
        }
        return success;
    }

    private void TryResumeGame()
    {
        if (!IsAnyPopupAlive() && GameManager.Instance.isInGame)
            PauseController.instance.TryResumeGame();
    }
}