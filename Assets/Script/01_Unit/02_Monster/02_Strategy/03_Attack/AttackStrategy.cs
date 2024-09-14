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

    protected void CheckGround()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(CurrentPos() + offset, Vector3.right * direction(), 1.5f, TargetLayer);

    }
}
