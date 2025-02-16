using UnityEngine;

public class GergusAnimatorController : StateMachineBehaviour
{
    private BossGergus bossGergus;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossGergus == null) bossGergus = animator.GetComponent<BossGergus>();

        if (stateInfo.IsName(MonsterAnimation.Charge)) bossGergus.SetStatus(MonsterStatus.Attack);
        else if (stateInfo.IsName(MonsterAnimation.Attack)) bossGergus.AttackStart();
        else if (stateInfo.IsName(MonsterAnimation.AttackEnd)) bossGergus.AttackEnd();
        else if (stateInfo.IsName(MonsterAnimation.Groggy)) bossGergus.SetStatus(MonsterStatus.Groggy);
        else if (stateInfo.IsName(MonsterAnimation.Hurt))
        {
            bossGergus.PlayScarEffect();
        }
        else if (stateInfo.IsName(MonsterAnimation.Die))
        {
            bossGergus.SetStatus(MonsterStatus.Dead);
            bossGergus.PlayScarEffect();
            bossGergus.MakePlayerRewards();
            Destroy(bossGergus.gameObject, MonsterConstant.monsterDieRemoveTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(MonsterAnimation.Hurt))
        {
            if (bossGergus.GetStatus() == MonsterStatus.Attack) bossGergus.SetStatus(MonsterStatus.Chase);
            else bossGergus.SetStatus(MonsterStatus.Idle);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(MonsterAnimation.Attack)) bossGergus.AttackUpdate();
    }
}
