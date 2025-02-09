using System.Collections.Generic;

public class PopupManager : SingletonObject<PopupManager>
{
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
            PauseController.Instance.TryPauseGame();
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
            PauseController.Instance.TryResumeGame();
    }
}