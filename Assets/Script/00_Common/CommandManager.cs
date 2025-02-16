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
                        PopupManager.Instance.TurnOnGameExitPopup();
                    }
                }
            }

            if (isInGame)
            {
                if (TutorialManager.InstanceWithoutCreate != null)
                {
                    Player.Instance.CheckGround();
                    if (TutorialManager.InstanceWithoutCreate.isJumpAble)
                    {
                        Player.Instance.TryJump();
                    }
                    if (TutorialManager.InstanceWithoutCreate.IsWaitingForTutorialAction)
                    {
                        PlayerAction tutorialAction = TutorialManager.InstanceWithoutCreate.currentTutorialAction;
                        if (tutorialAction != PlayerAction.Null && Input.GetKeyDown(KeySetting.GetKey(tutorialAction)))
                        {
                            TutorialManager.InstanceWithoutCreate.SetUserInput(tutorialAction);
                        }
                    }
                }
                // When we paused the game, we don't want to check below commands.
                else if (!PauseController.Instance.IsPause() && Player.Instance != null)
                {
                    Player.Instance.CheckPlayerCommand();
                }

                if (Input.GetKeyDown(KeySetting.GetKey(PlayerAction.Interaction)))
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
        // if (!Util.IsEditor) return;
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            Inventory.Instance.ChangeSoulNumber(1000);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
            foreach (GameObject monster in monsters)
            {
                Monster monsterComponent = monster.GetComponent<Monster>();
                if (monsterComponent != null)
                {
                    monsterComponent.AttackedByPlayer(monsterComponent.GetCurrentStat(StatKind.HP), true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Portal portal = FindObjectOfType<Portal>();
            if (portal != null)
            {
                Player.Instance.transform.position = portal.transform.position;
            }
        }
    }
}