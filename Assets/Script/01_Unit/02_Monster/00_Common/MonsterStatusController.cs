using UnityEngine;

public class MonsterStatusController : StateMachineBehaviour
{
    private Monster monster;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster == null) monster = animator.GetComponent<Monster>();

        if (stateInfo.IsName("Idle")) monster.SetStatus(MonsterStatus.Idle);
        else if (stateInfo.IsName("Charge")) monster.SetStatus(MonsterStatus.AttackCharge);
        else if (stateInfo.IsName("Attack")) monster.SetStatus(MonsterStatus.Attack);
        else if (stateInfo.IsName("AttackEnd")) monster.SetStatus(MonsterStatus.AttackEnd);
        else if (stateInfo.IsName("Hurt")) monster.SetStatus(MonsterStatus.Hurt);
        else if (stateInfo.IsName("Die")) monster.SetStatus(MonsterStatus.Dead);
    }
}
