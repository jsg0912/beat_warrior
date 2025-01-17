using System.Collections;
using UnityEngine;

public abstract class AttackStrategy : Strategy
{
    protected MonoBehaviour monoBehaviour;
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
        if (monster.GetStatus() == MonsterStatus.Attack) return true;
        return TrySkill();
    }

    public void UpdateCoolTime()
    {
        if (attackCoolTime > 0 && monster.GetIsNotAttacking())
        {
            attackCoolTime -= Time.deltaTime;
        }
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

        attackCoolTime = attackCoolTimeMax;
        monster.SetStatus(MonsterStatus.Idle);
    }

    protected abstract void SkillMethod();
    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            monoBehaviour.StopCoroutine(attackCoroutine);
            monster.SetStatus(MonsterStatus.Idle);
        }
    }
}
