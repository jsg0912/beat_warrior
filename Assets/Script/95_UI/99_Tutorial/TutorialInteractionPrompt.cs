using UnityEngine;

public class TutorialInteractionPrompt : ObjectWithInteractionPrompt
{
    [SerializeField] private PlayerAction tutorialAction;
    [SerializeField] private bool NeedPause;

    override public bool StartInteraction()
    {
        if (NeedPause)
        {
            // 게임 재개
            PauseController.Instance.TryResumeGame();
            // Blur 제거
            BlurUIManager.Instance.TurnOffActiveBlur();
            if (!TutorialManager.Instance.isSkillAble)
                // Player Action
                Player.Instance.ForcePlayerAction(tutorialAction);
        }
        return true;
    }

    override public void SetInteractAble()
    {
        // Prompt 창 띄우기
        SetActivePromptText(true);
        if (promptText != null) promptText.text = PromptMessageGenerator.GeneratePromptMessage(tutorialAction);

        if (isInitialized) return;
        Initialize();

        // 게임 멈추기
        if (NeedPause)
        {
            PauseController.Instance.TryPauseGame();
            // Blur 켜기
            BlurUIManager.Instance.TurnOnActiveBlur(BlurType.SystemMessageBlackBlur);
            // Key Input 받도록 설정
            TutorialManager.Instance.SetCurrentTutorialAction(tutorialAction);
            // 설명창 켜기
            SystemMessageUIManager.Instance.TurnOnTutorialMassageUI(tutorialAction, true);
        }
        else
        {
            // 설명창 켜기
            SystemMessageUIManager.Instance.TurnOnTutorialMassageUI(tutorialAction, true);
        }
    }

    override public void SetInteractDisable()
    {
        SetActivePromptText(false);
    }

    public PlayerAction GetTutorialAction() => tutorialAction;
}
