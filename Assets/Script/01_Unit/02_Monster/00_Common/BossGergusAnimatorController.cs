using UnityEngine;

public class BossGergusAnimatorController : StateMachineBehaviour
{
    private BossGergus bossGergus;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossGergus == null) bossGergus = animator.GetComponent<BossGergus>();

        if (GetIsAttacking(stateInfo)) bossGergus.AttackStart();
        if (stateInfo.IsName(MonsterAnimation.AttackEnd))
        {
            bossGergus.AttackEnd();
            bossGergus.SetStatus(MonsterStatus.Idle);
            (bossGergus.pattern as PatternCh2Boss).ResetAttackCoolTimer();
        }
        else if (stateInfo.IsName(MonsterAnimation.Idle))
        {
            animator.ResetTrigger(MonsterAnimation.AttackEnd);
        }
        else if (stateInfo.IsName(MonsterAnimation.Hurt))
        {
            bossGergus.PlayScarEffect();
        }
        else if (stateInfo.IsName(MonsterAnimation.Die))
        {
            bossGergus.SetStatus(MonsterStatus.Dead);
            bossGergus.PlayScarEffect();
            Destroy(bossGergus.gameObject, MonsterConstant.monsterDieRemoveTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public bool GetIsAttacking(AnimatorStateInfo stateInfo)
    {
        return stateInfo.IsName("Left") || stateInfo.IsName("Right1") ||
               stateInfo.IsName("Slosh") || stateInfo.IsName("Crash") ||
               stateInfo.IsName("Throw");
    }
}
