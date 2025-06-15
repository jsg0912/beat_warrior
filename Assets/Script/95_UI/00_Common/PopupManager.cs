using UnityEngine;
using System.Collections.Generic;

public class PopupManager : SingletonObject<PopupManager>
{
    [SerializeField] private GameExitPopup gameExitPopup;
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private GameModeSettingPopup gameModeSettingPopup;
    private List<PopupSystem> popupSystemStack = new();

    public bool IsGameOverPopup => gameOverPopup.gameObject.activeSelf;

    private bool IsAnyPopupAlive()
    {
        return popupSystemStack.Count > 0;
    }

    private bool PopPopup()
    {
        bool success = popupSystemStack[popupSystemStack.Count - 1].TurnOffPopup();
        if (success)
        {
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

    public void TurnOnGameExitPopup()
    {
        gameExitPopup.TurnOnPopup();
    }

    public void TurnOnGameOverPopup()
    {
        gameOverPopup.TurnOnPopup();
    }

    public void TurnOnGameModeSettingPopup()
    {
        gameModeSettingPopup.TurnOnPopup();
    }
}