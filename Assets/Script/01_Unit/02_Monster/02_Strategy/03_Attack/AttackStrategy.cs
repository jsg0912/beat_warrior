using System.Collections;
using UnityEngine;

public abstract class AttackStrategy : Strategy
{
    protected MonoBehaviour monoBehaviour; // For Coroutine
    protected Direction attackDirection;
    protected Coroutine attackCoroutine;

    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    protected float attackStartDelay;
    protected float attackActionInterval;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        monoBehaviour = monster.GetComponent<MonoBehaviour>();

        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = 0;

        attackStartDelay = MonsterConstant.AttackStartDelays[monster.monsterName];
        attackActionInterval = MonsterConstant.AttackActionIntervals[monster.monsterName];
    }

    public override bool PlayStrategy()
    {
        if (monster.GetIsAttacking()) return true;
        return TrySkill();
    }

    public void UpdateCoolTime()
    {
        if (attackCoolTime > 0 && !monster.GetIsAttacking())
        {
            attackCoolTime -= Time.deltaTime;
        }
    }

    public void SetMaxAttackCoolTime()
    {
        attackCoolTime = attackCoolTimeMax;
    }

    protected bool TrySkill()
    {
        if (attackCoolTime > 0) return false;
        // [Code Review - KMJ]: Implement Attack Range Check Process - SDH, 20250119
        attackCoroutine = monoBehaviour.StartCoroutine(UseSkill());
        return true;
    }

    protected virtual IEnumerator UseSkill()
    {
        monster.SetStatus(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackStartDelay);

        monster.PlayAnimation(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackActionInterval);
        SkillMethod();

        SetMaxAttackCoolTime();
        monster.SetStatus(MonsterStatus.Idle);
    }

    protected abstract void SkillMethod();
    public virtual void StopAttack()
    {
        monoBehaviour.StopAllCoroutines();
        SetMaxAttackCoolTime();
    }

    public void SetAttackDirection()
    {
        attackDirection = monster.GetRelativeDirectionToPlayer();
    }
}
