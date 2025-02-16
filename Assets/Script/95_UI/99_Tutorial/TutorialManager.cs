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
        {PlayerAction.Tutorial_Mark, false},
        {PlayerAction.Tutorial_Dash, false},
        {PlayerAction.Interaction, false},
    };

    [SerializeField] private TutorialInteractionPrompt[] tutorialInteractionPrompts;

    public PlayerAction currentTutorialAction { get; private set; } = PlayerAction.Null;
    public bool IsTutorialComplete { get; private set; } = false;
    public bool IsWaitingForTutorialAction { get; private set; } = false;

    public void SetUserInput(PlayerAction action)
    {
        if (IsWaitingForTutorialAction && action == currentTutorialAction)
        {
            SetActionTutorialComplete(action);
            IsWaitingForTutorialAction = false;
            tutorialInteractionPrompts.First(x => x.GetTutorialAction() == action).StartInteraction();
        }
    }

    public void SetCurrentTutorialAction(PlayerAction action)
    {
        currentTutorialAction = action;
        IsWaitingForTutorialAction = true;
    }

    public void SetActionTutorialComplete(PlayerAction action)
    {
        tutorialList[action] = true;
        IsTutorialComplete = tutorialList.All(x => x.Value == true);
    }
}