using UnityEngine;

public class BossTentacleAnimationController : StateMachineBehaviour
{
    private Monster boss;
    private BossTentacle tentacle;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (tentacle == null) tentacle = animator.GetComponent<BossTentacle>();
        if (boss == null) boss = tentacle.Boss;
        if (stateInfo.IsName("End"))
        {
            if (tentacle.isFinalTentacle) boss.PlayAnimation(BossConstantCh2.AttackEndAnimTrigger);
            Util.SetActive(animator.gameObject, false);
        }
    }
}
