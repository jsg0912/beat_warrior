using UnityEngine;

public class TutorialInteractionPrompt : ObjectWithInteractionPrompt
{
    [SerializeField] private PlayerAction tutorialAction;

    override public bool StartInteraction()
    {
        PauseController.Instance.TryResumeGame();
        BlurUIManager.Instance.TurnOffActiveBlur();
        return true;
    }

    override public void SetInteractAble()
    {
        if (isInitialized) return;
        Initialize();
        SetActivePromptText(true);
        promptText.text = PromptMessageGenerator.GeneratePromptMessage(tutorialAction);
        PauseController.Instance.TryPauseGame();
        BlurUIManager.Instance.TurnOnActiveBlur(BlurType.BlackBlur);
        SystemMessageUIManager.Instance.TurnOnTutorialMassageUI(tutorialAction);
        TutorialManager.Instance.SetCurrentTutorialAction(tutorialAction);
    }

    override public void SetInteractDisable()
    {
        SetActivePromptText(false);
    }

    public PlayerAction GetTutorialAction() => tutorialAction;
}
