using UnityEngine;

public class PlayerAnimatorController : StateMachineBehaviour
{
    private Player player => Player.Instance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(PlayerAnimation.Dash) || stateInfo.IsName(PlayerAnimation.DashCharge)) player.SetStatus(PlayerStatus.Dash);
        if (stateInfo.IsName(PlayerAnimation.Idle)) player.SetStatus(PlayerStatus.Normal);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsAttackStatus(stateInfo)) player.InitializeAttackCollider();
        else if (stateInfo.IsName(PlayerAnimation.Revive)) player.SetStatus(PlayerStatus.Normal);
    }

    private bool IsAttackStatus(AnimatorStateInfo stateInfo)
    {
        return
        stateInfo.IsName(PlayerAnimation.AttackL)
        || stateInfo.IsName(PlayerAnimation.AttackR)
        || stateInfo.IsName(PlayerAnimation.Skill1)
        || stateInfo.IsName(PlayerAnimation.Skill2)
        || stateInfo.IsName(PlayerAnimation.Mark)
        || stateInfo.IsName(PlayerAnimation.Dash);
    }
}
