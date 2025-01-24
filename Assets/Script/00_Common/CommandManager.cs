using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;
    public List<PopupSystem> popupSystemStack = new(); // TODO: PopupController - SDH, 20250124

    void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isInGame && PauseController.instance.GetPause() == false)
        {
            Player.Instance?.CheckIsMove();
        }
    }

    void Update()
    {
        bool isInGame = GameManager.Instance.isInGame;
        bool isCommandAble = GameManager.Instance.IsLoading == false;

        if (isCommandAble)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log($"Escape Key Pressed: {popupSystemStack.Count}");
                if (popupSystemStack.Count > 0)
                {
                    popupSystemStack[popupSystemStack.Count - 1].TurnOffPopup();
                }
                if (isInGame)
                {
                    if (popupSystemStack.Count == 0)
                    {
                        PauseController.instance.PauseGame(); // TODO: 기획 정해야함, MenuUI 뿐만 아닌 모든 PopupSystem이 켜져야하는 경우 InGame이 멈춰야 하는지 확인 후, 해당 Logic 위치 수정 필요.
                        MenuUI.Instance.TurnOnPopup();
                    }
                }
            }

            if (isInGame)
            {
                // When we paused the game, we don't want to check below commands.
                if (!PauseController.instance.GetPause() && Player.Instance != null)
                {
                    Player.Instance.CheckPlayerCommand();
                }

                if (Input.GetKeyDown(KeySetting.keys[PlayerAction.Interaction]))
                {
                    // [Code Review - LJD] TODO: UIManager에서 AltarPopup을 키는 것이 아님 + 주변에 InteractionPressPrompt가 있는지 확인해야 함 + Player의 Interaction()도 함께 고려해야함 - SDH, 20250117
                    UIManager.Instance.TurnOnAltarPopup();
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    // TODO: Test용 임시 재시작(부활) 코드로 삭제해야 함. - SDH, 20250124
                    GameManager.Instance.RestartGame();
                }
            }
        }
    }

    public void PopPopupSystem()
    {
        if (popupSystemStack.Count > 0)
        {
            popupSystemStack.RemoveAt(popupSystemStack.Count - 1);
            if (GameManager.Instance.isInGame && popupSystemStack.Count == 0)
            {
                PauseController.instance.ResumeGame();
            }
        }
    }
}