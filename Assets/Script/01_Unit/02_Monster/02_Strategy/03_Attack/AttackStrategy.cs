using UnityEngine;

public class AttackStrategy : Strategy
{
    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = attackCoolTimeMax;
    }

    private void CheckCoolTime()
    {
        if (attackCoolTime >= 0) attackCoolTime -= Time.deltaTime;
    }
}
