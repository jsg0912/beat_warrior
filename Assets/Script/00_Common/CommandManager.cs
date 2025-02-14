using UnityEngine;

public class CommandManager : SingletonObject<CommandManager>
{
    void FixedUpdate()
    {
        if (GameManager.Instance.isInGame && !PauseController.Instance.IsPause())
        {
            Player.Instance?.CheckIsMove();
        }
    }

    void Update()
    {
        bool isInGame = GameManager.Instance.isInGame;
        bool isCommandAble = !GameManager.Instance.IsLoading;

        if (isCommandAble)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!PopupManager.Instance.TryPopPopup())
                {
                    if (isInGame)
                    {
                        PauseController.Instance.TryPauseGame(); // TODO: 기획 정해야함, MenuUI 뿐만 아닌 모든 PopupSystem이 켜져야하는 경우 InGame이 멈춰야 하는지 확인 후, 해당 Logic 위치 수정 필요.
                        MenuUI.Instance.TurnOnPopup();
                    }
                    else
                    {
                        // TODO: Game Exit Popup 띄우기
                    }
                }
            }

            if (isInGame)
            {
                // When we paused the game, we don't want to check below commands.
                if (!PauseController.Instance.IsPause() && Player.Instance != null)
                {
                    Player.Instance.CheckPlayerCommand();
                }

                if (Input.GetKeyDown(KeySetting.keys[PlayerAction.Interaction]))
                {
                    InteractionManager.Instance.InteractWithLastObject();
                }
                CheckTestCommandInGame();
            }
        }
    }

    // TODO: 이 아래는 Test용 임시 코드들로 삭제해야 함. - SDH, 20250124
    public void CheckTestCommandInGame()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.Instance.RestartCurrentStage();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Player.Instance.GetDamaged(Player.Instance.GetCurrentStat(StatKind.HP), Player.Instance.GetMovingDirection());
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PauseController.Instance.ChangeDefaultGameSpeed(1f);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PauseController.Instance.ChangeDefaultGameSpeed(0.3f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseController.Instance.ChangeDefaultGameSpeed(0.1f);
        }
    }
}