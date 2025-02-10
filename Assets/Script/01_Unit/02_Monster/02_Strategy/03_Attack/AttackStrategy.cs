using UnityEngine;

public abstract class AttackStrategy : Strategy
{
    protected MonoBehaviour monoBehaviour; // For Coroutine
    protected Direction attackDirection;
    protected Coroutine attackCoroutine;

    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        monoBehaviour = monster.GetComponent<MonoBehaviour>();

        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = 0;
    }

    public override bool PlayStrategy()
    {
        if (monster.GetIsAttacking()) return true;
        return TrySkill();
    }

    public void UpdateCoolTime()
    {
        if (attackCoolTime > 0 && !monster.GetIsAttacking()) attackCoolTime -= Time.deltaTime;
    }

    public void SetMaxAttackCoolTime()
    {
        attackCoolTime = attackCoolTimeMax;
    }

    protected bool TrySkill()
    {
        if (attackCoolTime > 0) return false;

        SetAttackDirection();
        monster.PlayAnimation(MonsterStatus.Attack);
        return true;
    }

    public virtual void AttackStart()
    {
        SkillMethod();
        Debug.Log(monster.monsterName + "attackstart");
    }

    public virtual void AttackEnd()
    {
        SetMaxAttackCoolTime();
        Debug.Log(monster.monsterName + "attackend");
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
        monster.SetMovingDirection(attackDirection);
    }
}
