using UnityEngine;

public class BossTentacleAnimationController : StateMachineBehaviour
{
    private Monster boss;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss == null) boss = animator.GetComponent<BossTentacle>().Boss;
        if (stateInfo.IsName("Attack")) boss.PlayAnimation(MonsterAnimation.AttackEnd);
    }
}
