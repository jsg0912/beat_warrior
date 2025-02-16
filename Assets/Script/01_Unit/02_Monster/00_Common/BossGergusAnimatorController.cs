using UnityEngine;

public class BossGergusAnimatorController : StateMachineBehaviour
{
    private BossGergus bossGergus;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossGergus == null) bossGergus = animator.GetComponent<BossGergus>();

        if (stateInfo.IsName(MonsterAnimation.AttackEnd))
        {
            bossGergus.AttackEnd();
            (bossGergus.pattern as PatternCh2Boss).ResetAttackCoolTimer();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
