using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialManager : SingletonObject<TutorialManager>
{
    private Dictionary<PlayerAction, bool> tutorialList = new(){
        {PlayerAction.Attack, false},
        {PlayerAction.Skill1, false},
        {PlayerAction.Skill2, false},
        {PlayerAction.Jump, false},
        {PlayerAction.Down, false},
        {PlayerAction.Tutorial_Mark, false},
        {PlayerAction.Tutorial_Dash, false},
        {PlayerAction.Tutorial_Portal, false},
    };

    [SerializeField] private TutorialInteractionPrompt[] tutorialInteractionPrompts;

    public PlayerAction currentTutorialAction { get; private set; } = PlayerAction.Null;
    public bool IsTutorialComplete { get; private set; } = false;
    public bool IsWaitingForTutorialAction => currentTutorialAction != PlayerAction.Null;

    public void SetUserInput(PlayerAction action)
    {
        if (!tutorialList.ContainsKey(action)) return;
        if (IsWaitingForTutorialAction && action == currentTutorialAction)
        {
            SetActionTutorialComplete(action);
            tutorialInteractionPrompts.First(x => x.GetTutorialAction() == action).StartInteraction();
            currentTutorialAction = PlayerAction.Null;
        }
    }

    public void SetCurrentTutorialAction(PlayerAction action)
    {
        currentTutorialAction = action;
    }

    public void SetActionTutorialComplete(PlayerAction action)
    {
        tutorialList[action] = true;
        UpdateIsTutorialComplete();
    }

    private void UpdateIsTutorialComplete()
    {
        IsTutorialComplete = tutorialList.All(x => x.Value == true);
        if (IsTutorialComplete)
        {
            ChapterManager.Instance.SetTutorialComplete();
        }
    }
}