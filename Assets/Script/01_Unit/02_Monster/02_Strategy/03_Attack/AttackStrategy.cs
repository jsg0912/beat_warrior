using UnityEngine;

public abstract class AttackStrategy : Strategy
{
    protected MonoBehaviour monoBehaviour; // For Coroutine
    protected Direction attackDirection;
    protected Coroutine attackCoroutine;

    protected float attackRange;
    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    public AttackStrategy(string monsterAnimTrigger)
    {
        this.monsterAnimTrigger = monsterAnimTrigger;
    }

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        monoBehaviour = monster.GetComponent<MonoBehaviour>();

        attackRange = MonsterConstant.AttackRange[monster.monsterName];
        attackCoolTimeMax = MonsterConstant.AttackCoolTime[monster.monsterName];
        attackCoolTime = 0;
    }

    public override bool PlayStrategy()
    {
        if (monster.GetIsAttacking()) return true;
        if (!monster.GetIsAttackAble()) return false;
        return TryAttack();
    }

    // Attack을 시도해서 가능한 상황이면, AttackCharge Animation을 시작하고, AnimatorController에서 그 후 처리를 시작함.
    protected virtual bool TryAttack()
    {
        if (attackCoolTime > 0 || !IsInAttackRange()) return false;

        SetAttackDirection();
        StartAttackCharge();
        return true;
    }

    protected virtual void StartAttackCharge() { monster.PlayAnimation(monsterAnimTrigger); }

    // 이 함수가 실행되면 정말로 공격을 실행한는 것으로 간주
    protected abstract void AttackMethod();

    public virtual void StopAttack()
    {
        monoBehaviour.StopAllCoroutines();
    }

    public void SetAttackDirection()
    {
        attackDirection = monster.GetRelativeDirectionToPlayer();
        monster.SetMovingDirection(attackDirection);
    }

    public virtual void AttackStart()
    {
        if (MonsterConstant.NotKnockBackAbleWhenAttacking.Contains(monster.monsterName)) monster.SetIsKnockBackAble(false);
        if (MonsterConstant.FixedAnimationWhenAttacking.Contains(monster.monsterName)) monster.SetIsFixedAnimation(true);
        SetMaxAttackCoolTime();
        AttackMethod();
    }

    public virtual void AttackUpdate() { }

    public virtual void AttackEnd()
    {
        // 주의: base를 안쓰기 떄문에, base에서 공용으로 써야하는 것들이 새로 생기면, 여기도 추가해야할 수 있다.
        monster.SetIsKnockBackAble(true);
        monster.SetIsFixedAnimation(false);
        monster.SetStatus(MonsterStatus.Chase); // TODO: AttackEnd 후에 무조건 Chase로 가야하는지 확인 필요
    }
    public void UpdateCoolTime() { if (attackCoolTime > 0 && !monster.GetIsAttacking()) attackCoolTime -= Time.deltaTime; }
    public void SetMaxAttackCoolTime() { attackCoolTime = attackCoolTimeMax; }
    protected virtual bool IsInAttackRange() { return Vector2.Distance(GetPlayerPos(), GetMonsterPos()) < attackRange; }
}
