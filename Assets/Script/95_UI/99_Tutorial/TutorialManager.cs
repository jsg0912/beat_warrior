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
    };

    //TODO: 시간 없어서 아래처럼 함
    public bool isJumpAble { private set; get; } = false;
    public bool isSkillAble { private set; get; } = false;

    [SerializeField] private TutorialInteractionPrompt[] tutorialInteractionPrompts;

    public PlayerAction currentTutorialAction { get; private set; } = PlayerAction.Null;
    public bool IsTutorialComplete { get; private set; } = false;
    public bool IsWaitingForTutorialAction => currentTutorialAction != PlayerAction.Null;

    public void SetUserInput(PlayerAction action)
    {
        if (!tutorialList.ContainsKey(action)) return;
        if (IsWaitingForTutorialAction && action == currentTutorialAction)
        {
            tutorialInteractionPrompts.First(x => x.GetTutorialAction() == action).StartInteraction();
            currentTutorialAction = PlayerAction.Null;

            if (action == PlayerAction.Jump)
            {
                isJumpAble = true;
            }
            if (tutorialList[PlayerAction.Skill1] && tutorialList[PlayerAction.Skill2])
            {
                isSkillAble = true;
            }
        }
    }

    public void SetCurrentTutorialAction(PlayerAction action)
    {
        currentTutorialAction = action;
    }

    public void SetActionTutorialComplete(PlayerAction action)
    {
        if (!tutorialList.ContainsKey(action)) return;
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

    public void CheckTutorialKey()
    {
        if (isSkillAble)
        {
            Player.Instance.CheckPlayerCommand();
        }
        else
        {
            Player.Instance.CheckGround();
            if (isJumpAble)
            {
                Player.Instance.TryJump();
            }
        }

        if (IsWaitingForTutorialAction)
        {
            PlayerAction tutorialAction = currentTutorialAction;
            if (tutorialAction != PlayerAction.Null && Input.GetKeyDown(KeySetting.GetKey(tutorialAction)))
            {
                SetUserInput(tutorialAction);
            }
        }
    }
}