using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;
    public List<PopupSystem> popupSystemStack = new();

    void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        if (!PauseController.instance.GetPause())
        {
            Player.Instance.CheckIsMove();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popupSystemStack.Count == 0)
            {
                PauseController.instance.PauseGame();
                MenuUI.Instance.TurnOnPopup();
            }
            else
            {
                popupSystemStack[popupSystemStack.Count - 1].TurnOffPopup();
                popupSystemStack.RemoveAt(popupSystemStack.Count - 1);
                if (popupSystemStack.Count == 0)
                {
                    PauseController.instance.ResumeGame();
                }
            }
        }

        if (Input.GetKeyDown(KeySetting.keys[PlayerAction.Interaction]))
        {
            // TODO: UIManager에서 AltarPopup을 키는 것이 아님 + 주변에 InteractionPressPrompt가 있는지 확인해야 함. - SDH, 20250117
            UIManager.Instance.TurnOnAltarPopup();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // TODO: 임시 재시작(부활) 코드
            GameManager.Instance.RestartGame();
        }

        // When we paused the game, we don't want to check below commands.
        if (!PauseController.instance.GetPause())
        {
            Player.Instance.CheckPlayerCommand();
        }
    }
}