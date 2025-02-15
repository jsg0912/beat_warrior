using UnityEngine;

public class MonsterAnimatorController : StateMachineBehaviour
{
    private Monster monster;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster == null) monster = animator.GetComponent<Monster>();

        if (stateInfo.IsName(MonsterAnimation.Charge)) monster.SetStatus(MonsterStatus.Attack);
        else if (stateInfo.IsName(MonsterAnimation.Attack)) monster.AttackStart();
        else if (stateInfo.IsName(MonsterAnimation.AttackEnd)) monster.AttackEnd();
        else if (stateInfo.IsName(MonsterAnimation.Groggy)) monster.SetStatus(MonsterStatus.Groggy);
        else if (stateInfo.IsName(MonsterAnimation.Hurt))
        {
            monster.PlayScarEffect();
        }
        else if (stateInfo.IsName(MonsterAnimation.Die))
        {
            monster.SetStatus(MonsterStatus.Dead);
            monster.PlayScarEffect();
            monster.MakePlayerRewards();
            Destroy(monster.gameObject, MonsterConstant.monsterDieRemoveTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(MonsterAnimation.Hurt))
        {
            if (monster.GetStatus() == MonsterStatus.Attack) monster.SetStatus(MonsterStatus.Chase);
            else monster.SetStatus(MonsterStatus.Idle);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName(MonsterAnimation.Attack)) monster.AttackUpdate();
    }
}
