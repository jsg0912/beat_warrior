using UnityEngine;

public class BossGergusAnimatorController : StateMachineBehaviour
{
    private BossGergus bossGurges;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossGurges == null) bossGurges = animator.GetComponent<BossGergus>();

        if (IsAttackInfo(stateInfo)) bossGurges.AttackStart();
        if (stateInfo.IsName(MonsterAnimation.AttackEnd))
        {
            bossGurges.AttackEnd();
            bossGurges.SetStatus(MonsterStatus.Idle);
            (bossGurges.pattern as PatternBossGurges).ResetAttackCoolTimer();
        }
        else if (stateInfo.IsName(MonsterAnimation.Idle))
        {
            animator.ResetTrigger(MonsterAnimation.AttackEnd);
        }
        else if (stateInfo.IsName("Stand"))
        {
            bossGurges.SetAnimationFloat(BossConstantCh2.IsStandAnimBool, 1.0f);
        }
        else if (stateInfo.IsName(MonsterAnimation.Die))
        {
            bossGurges.SetStatus(MonsterStatus.Dead);
        }
        else if (stateInfo.IsName(MonsterAnimation.DieEnd))
        {
            Destroy(bossGurges.gameObject);
            // TODO: TBD
            SystemMessageUIManager.Instance.TurnOnSystemMassageUI(SystemMessageType.ToBeContinued, 10.0f);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Stand"))
        {
            bossGurges.SetStatus(MonsterStatus.Idle);
        }
    }

    private bool IsAttackInfo(AnimatorStateInfo stateInfo)
    {
        return stateInfo.IsName("Left") || stateInfo.IsName("Right1") ||
               stateInfo.IsName("Slosh") || stateInfo.IsName("Crash") ||
               stateInfo.IsName("Throw");
    }
}
