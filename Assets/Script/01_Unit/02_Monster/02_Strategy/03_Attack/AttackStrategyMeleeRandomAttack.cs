using System.Collections.Generic;
using UnityEngine;

public class AttackStrategyMeleeRandomAttack : AttackStrategy
{
    protected List<MonsterAttackCollider> attackColliders = new List<MonsterAttackCollider>();
    MonsterAttackCollider attackCollider;
    public AttackStrategyMeleeRandomAttack(string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger,
    List<MonsterAttackCollider> attackColliders = null) : base(monsterAnimTrigger)
    {
        this.attackColliders = attackColliders;
    }

    public AttackStrategyMeleeRandomAttack(string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger,
    List<BossTentacle> attackColliders = null) : base(monsterAnimTrigger)
    {
        attackColliders.ForEach(attackCollider => this.attackColliders.Add(attackCollider));
    }


    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
    }

    public override void AttackStart()
    {
        attackCollider = attackColliders[Random.Range(0, attackColliders.Count)];
        attackCollider.SetMonsterAtk(monster.GetFinalStat(StatKind.ATK));
        Util.SetActive(attackCollider.gameObject, true);
    }

    public override void AttackEnd()
    {
        monster.PlayAnimation(MonsterAnimTrigger.attackEndAnimTrigger);
        Util.SetActive(attackCollider.gameObject, false);
    }

    protected override void AttackMethod()
    {
    }
}