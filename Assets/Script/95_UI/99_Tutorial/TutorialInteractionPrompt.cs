using UnityEngine;

public class TutorialInteractionPrompt : ObjectWithInteractionPrompt
{
    [SerializeField] private PlayerAction tutorialAction;

    override public bool StartInteraction()
    {
        // 게임 재개
        PauseController.Instance.TryResumeGame();
        // Blur 제거
        BlurUIManager.Instance.TurnOffActiveBlur();
        return true;
    }

    override public void SetInteractAble()
    {
        if (isInitialized) return;
        Initialize();
        // Prompt 창 띄우기
        SetActivePromptText(true);
        promptText.text = PromptMessageGenerator.GeneratePromptMessage(tutorialAction);
        // 게임 멈추기
        PauseController.Instance.TryPauseGame();
        // Blur 및 설명창 켜기
        SystemMessageUIManager.Instance.TurnOnTutorialMassageUI(tutorialAction);
        BlurUIManager.Instance.TurnOnActiveBlur(BlurType.BlackBlur);
        // Key Input 받도록 설정
        TutorialManager.Instance.SetCurrentTutorialAction(tutorialAction);
    }

    override public void SetInteractDisable()
    {
        SetActivePromptText(false);
    }

    public PlayerAction GetTutorialAction() => tutorialAction;
}
