using UnityEngine;

public class PlayerAnimatorController : StateMachineBehaviour
{
    private Player player => Player.Instance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsAttackStatus(stateInfo)) player.AttackAnimationStart();
        if (stateInfo.IsName(PlayerAnimation.Dash) || stateInfo.IsName(PlayerAnimation.DashCharge)) player.SetStatus(PlayerStatus.Dash);
        else if (stateInfo.IsName(PlayerAnimation.Idle)) player.SetStatus(PlayerStatus.Normal);
        else if (stateInfo.IsName(PlayerAnimation.DieEnd)) PopupManager.Instance.TurnOnGameOverPopup();
        else if (stateInfo.IsName(PlayerAnimation.Revive1)) (player.HaveSkill(SkillName.Revive) as Revive).ReviveFunctionBefore();
        else if (stateInfo.IsName(PlayerAnimation.Rest1) || stateInfo.IsName(PlayerAnimation.Rest2)) player.SetStatus(PlayerStatus.Rest);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(PlayerAnimation.Fall) && player.CheckWall()) animator.SetBool("isWall", true);
        if (stateInfo.IsName(PlayerAnimation.FallWall) && !player.CheckWall()) animator.SetBool("isWall", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsAttackStatus(stateInfo))
        {
            player.InitializeAttackCollider();
            player.AttackAnimationEnd();
        }
    }

    private bool IsAttackStatus(AnimatorStateInfo stateInfo)
    {
        return
        stateInfo.IsName(PlayerAnimation.SpecialBlade1)
        || stateInfo.IsName(PlayerAnimation.SpecialBlade2)
        || stateInfo.IsName(PlayerAnimation.Attack)
        || stateInfo.IsName(PlayerAnimation.SweepingBlade)
        || stateInfo.IsName(PlayerAnimation.Mark)
        || stateInfo.IsName(PlayerAnimation.Dash);
    }
}
