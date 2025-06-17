using System.Collections.Generic;
using UnityEngine;

public class TutorialInteractionPrompt : ObjectWithInteractionPrompt
{
    [SerializeField] private List<PlayerAction> tutorialActions;
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
                Player.Instance.ForcePlayerAction(GetTutorialAction());
        }
        return true;
    }

    override public void SetInteractAble()
    {
        // Prompt 창 띄우기
        SetActivePromptText(true);
        if (promptText != null) promptText.text = PromptMessageGenerator.GeneratePromptMessage(GetTutorialAction());

        if (isInitialized) return;
        Initialize();

        foreach (var action in tutorialActions)
        {
            TutorialManager.Instance.SetActionTutorialComplete(action);
        }

        // 게임 멈추기
        if (NeedPause)
        {
            PauseController.Instance.TryPauseGame();
            // Blur 켜기
            BlurUIManager.Instance.TurnOnActiveBlur(BlurType.SystemMessageBlackBlur);
            // Key Input 받도록 설정
            TutorialManager.Instance.SetCurrentTutorialAction(GetTutorialAction());

            TurnOnTutorialMassageUI();
        }
        else
        {
            TurnOnTutorialMassageUI();
        }
    }

    // 설명창 켜기
    private void TurnOnTutorialMassageUI()
    {
        SystemMessageUIManager.Instance.TurnOnTutorialMassageUI(GetTutorialAction(), true);
    }

    override public void SetInteractDisable()
    {
        SetActivePromptText(false);
    }

    public PlayerAction GetTutorialAction() => tutorialActions[0];
}