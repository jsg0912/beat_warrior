using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStrategyTentaclePress : AttackStrategy
{
    protected List<BossTentacle> leftTentacles = new List<BossTentacle>();
    protected List<BossTentacle> rightTentacles = new List<BossTentacle>();
    MonsterAttackCollider attackCollider;
    public AttackStrategyTentaclePress(List<BossTentacle> leftTentacles, List<BossTentacle> rightTentacles, string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger) : base(monsterAnimTrigger)
    {
        this.leftTentacles = leftTentacles;
        this.rightTentacles = rightTentacles;
    }

    public override void AttackStart()
    {
        AttackMethod();
    }

    public override void AttackEnd()
    {
        Util.SetActive(attackCollider.gameObject, false);
    }

    protected override void AttackMethod()
    {
        if (monster.GetRelativeDirectionToPlayer() == Direction.Left)
        {
            attackCoroutine = monoBehaviour.StartCoroutine(AttackCoroutine(leftTentacles));
        }
        else
        {
            attackCoroutine = monoBehaviour.StartCoroutine(AttackCoroutine(rightTentacles));
        }
    }

    private IEnumerator AttackCoroutine(List<BossTentacle> tentacles)
    {
        int index = 0;
        while (index < tentacles.Count)
        {
            yield return new WaitForSeconds(BossConstantCh2.TentacleAnimationDelay);
            attackCollider = tentacles[index++];
            attackCollider.SetMonsterAtk(monster.GetFinalStat(StatKind.ATK));
            Util.SetActive(attackCollider.gameObject, true);
        }
    }
}