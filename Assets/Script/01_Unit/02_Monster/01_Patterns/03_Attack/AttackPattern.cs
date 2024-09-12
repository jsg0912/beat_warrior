using System.Collections;
using UnityEngine;

public class AttackPattern : Pattern
{
    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    public override void Initialize(GameObject gameObject)
    {
        this.gameObject = gameObject;
        monster = gameObject.GetComponent<Monster>();

        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = attackCoolTimeMax;
    }

    private void CheckCoolTime()
    {
        if (attackCoolTime >= 0) attackCoolTime -= Time.deltaTime;
    }
}
