using UnityEngine;

public abstract class AttackStrategy : Strategy
{
    protected MonoBehaviour monoBehaviour; // For Coroutine
    protected Direction attackDirection;
    protected Coroutine attackCoroutine;

    protected float attackRange;
    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        monoBehaviour = monster.GetComponent<MonoBehaviour>();

        attackRange = MonsterConstant.AttackRange[monster.monsterName];
        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = 0;
    }

    public override bool PlayStrategy()
    {
        if (monster.GetIsAttacking()) return true;
        return TrySkill();
    }

    protected virtual bool TrySkill()
    {
        if (attackCoolTime > 0) return false;
        if (!CheckTarget()) return false;

        SetAttackDirection();
        monster.PlayAnimation(MonsterStatus.Attack);
        return true;
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

    public virtual void AttackStart() { SkillMethod(); }
    public virtual void AttackUpdate() { }
    public virtual void AttackEnd() { SetMaxAttackCoolTime(); }
    public void UpdateCoolTime() { if (attackCoolTime > 0 && !monster.GetIsAttacking()) attackCoolTime -= Time.deltaTime; }
    public void SetMaxAttackCoolTime() { attackCoolTime = attackCoolTimeMax; }
    protected virtual bool CheckTarget() { return Vector2.Distance(GetPlayerPos(), GetMonsterPos()) < attackRange; }
}
