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

    protected bool isAttacking = false;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        monoBehaviour = monster.GetComponent<MonoBehaviour>();

        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = attackCoolTimeMax;

        attackStartDelay = MonsterConstant.AttackStartDelays[monster.monsterName];
        attackActionInterval = MonsterConstant.AttackActionIntervals[monster.monsterName];
    }

    public override void PlayStrategy()
    {
        if (isAttacking == true) return;

        CheckCoolTime();
        TrySkill();
    }

    protected void CheckCoolTime()
    {
        if (attackCoolTime >= 0) attackCoolTime -= Time.deltaTime;
    }

    protected void TrySkill()
    {
        if (attackCoolTime >= 0) return;

        attackCoroutine = monoBehaviour.StartCoroutine(UseSkill());
    }

    protected virtual IEnumerator UseSkill()
    {
        isAttacking = true;

        monster.SetIsMoveable(false);
        yield return new WaitForSeconds(attackStartDelay);

        monster.PlayAnimation(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackActionInterval);
        SkillMethod();

        monster.SetIsMoveable(true);

        isAttacking = false;
        attackCoolTime = attackCoolTimeMax;
    }

    protected abstract void SkillMethod();
    public void StopAttack() { monoBehaviour.StopCoroutine(attackCoroutine); }
}
