using System.Collections;
using UnityEngine;

public abstract class AttackStrategy : Strategy
{
    protected MonoBehaviour monoBehaviour;

    protected float attackCoolTimeMax;
    protected float attackCoolTime;

    protected float attackDelay;
    protected float animationDelay;

    protected bool isAttacking = false;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        monoBehaviour = monster.GetComponent<MonoBehaviour>();

        attackCoolTimeMax = MonsterConstant.AttackSpeed[monster.monsterName];
        attackCoolTime = attackCoolTimeMax;

        attackDelay = MonsterConstant.AttackDelay[monster.monsterName];
        animationDelay = MonsterConstant.AnimationDelay[monster.monsterName];
    }

    public override void PlayStrategy()
    {
        if (monster.GetStatus() != MonsterStatus.Chase) return;
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

        monoBehaviour.StartCoroutine(UseSkill());
    }

    protected virtual IEnumerator UseSkill()
    {
        isAttacking = true;

        monster.SetIsMoveable(false);
        yield return new WaitForSeconds(attackDelay);

        SkillMethod();
        yield return new WaitForSeconds(animationDelay);

        monster.SetIsMoveable(true);

        isAttacking = false;
        attackCoolTime = attackCoolTimeMax;
    }

    protected abstract void SkillMethod();

    protected void CheckGround()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(CurrentPos() + offset, Vector3.right * direction(), 1.5f, TargetLayer);

    }
}
