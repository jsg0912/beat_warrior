using UnityEngine;

public class CommandManager : SingletonObject<CommandManager>
{
    bool isCheatingMode = false;
    private string targetWord = "Nights";
    private int currentIndex = 0;

    void FixedUpdate()
    {
        if (GameManager.Instance.isInGame && !PauseController.Instance.IsPause() && !SystemMessageUIManager.Instance.isTimeLinePlaying)
        {
            Player.Instance?.CheckIsMove();
        }
    }

    void Update()
    {
        if (!isCheatingMode && Input.anyKeyDown) // 키 입력 감지
        {
            string keyPressed = Input.inputString; // 입력된 키 저장

            if (!string.IsNullOrEmpty(keyPressed)) // 키 입력이 비어 있지 않은 경우
            {
                if (keyPressed[0] == targetWord[currentIndex]) // 올바른 순서로 입력했는지 확인
                {
                    currentIndex++; // 다음 문자로 진행

                    if (currentIndex == targetWord.Length) // 전체 단어를 올바르게 입력했을 경우
                    {
                        isCheatingMode = true;
                    }
                }
                else // 틀린 문자 입력 시 초기화
                {
                    currentIndex = 0; // 다시 처음부터 입력해야 함
                }
            }
        }

        bool isInGame = GameManager.Instance.isInGame;
        bool isCommandAble = !GameManager.Instance.IsLoading;

        if (isCommandAble)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!PopupManager.Instance.TryPopPopup())
                {
                    if (isInGame && !PopupManager.Instance.IsGameOverPopup)
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

            if (isInGame && !SystemMessageUIManager.Instance.isTimeLinePlaying)
            {
                if (TutorialManager.InstanceWithoutCreate != null)
                {
                    TutorialManager tutorialManager = TutorialManager.InstanceWithoutCreate;
                    if (tutorialManager.isSkillAble)
                    {
                        Player.Instance.CheckPlayerCommand();
                    }
                    else
                    {
                        Player.Instance.CheckGround();
                        if (tutorialManager.isJumpAble)
                        {
                            Player.Instance.TryJump();
                        }
                    }

                    if (tutorialManager.IsWaitingForTutorialAction)
                    {
                        PlayerAction tutorialAction = tutorialManager.currentTutorialAction;
                        if (tutorialAction != PlayerAction.Null && Input.GetKeyDown(KeySetting.GetKey(tutorialAction)))
                        {
                            tutorialManager.SetUserInput(tutorialAction);
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
        if (!isCheatingMode) return;
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