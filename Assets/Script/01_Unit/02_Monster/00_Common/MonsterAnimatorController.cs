using UnityEngine;

public class MonsterAnimatorController : StateMachineBehaviour
{
    private Monster monster;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster == null) monster = animator.GetComponent<Monster>();

        if (stateInfo.IsName(MonsterAnimation.Idle)) monster.SetStatus(MonsterStatus.Idle);
        else if (stateInfo.IsName(MonsterAnimation.Charge)) monster.SetStatus(MonsterStatus.AttackCharge);
        else if (stateInfo.IsName(MonsterAnimation.Attack)) monster.SetStatus(MonsterStatus.Attack);
    }
}
