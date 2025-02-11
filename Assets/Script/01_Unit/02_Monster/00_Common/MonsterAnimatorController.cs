using UnityEngine;

public class MonsterAnimatorController : StateMachineBehaviour
{
    private Monster monster;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster == null) monster = animator.GetComponent<Monster>();

        if (stateInfo.IsName(MonsterAnimation.Idle) || stateInfo.IsName(MonsterAnimation.Walk)) monster.SetStatus(MonsterStatus.Normal);
        else if (stateInfo.IsName(MonsterAnimation.Charge)) monster.SetStatus(MonsterStatus.Attack);
        else if (stateInfo.IsName(MonsterAnimation.Attack)) monster.AttackStart();
        else if (stateInfo.IsName(MonsterAnimation.AttackEnd)) monster.AttackEnd();
        else if (stateInfo.IsName(MonsterAnimation.Groggy)) monster.SetStatus(MonsterStatus.Groggy);
        else if (stateInfo.IsName(MonsterAnimation.Die)) monster.SetStatus(MonsterStatus.Dead);
    }
}
