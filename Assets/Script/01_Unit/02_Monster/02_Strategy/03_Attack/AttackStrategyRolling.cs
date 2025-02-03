using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AttackStrategyRolling : AttackStrategy
{
    public float jumpPower;
    public int numJumps;
    public float duration;

    protected override IEnumerator UseSkill()
    {
        monster.SetStatus(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackStartDelay);

        monster.PlayAnimation(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackActionInterval);
        SkillMethod();

        yield return new WaitForSeconds(duration + 0.3f);
        SetAfterSkill();

        monster.PlayAnimation(MonsterStatus.AttackEnd);
    }

    protected override void SkillMethod()
    {
        Vector3 target = Player.Instance.transform.position;
        target.y = monster.transform.position.y + jumpPower;
        SetBeforeSkill();

        monster.transform.DOJump(target, jumpPower, numJumps, duration)
            .SetEase(Ease.OutQuad); // 부드러운 튀는 느낌
    }

    private void SetBeforeSkill()
    {
        monster.SetStatus(MonsterStatus.Attack);
        SetAttackDirection();

        monster.SetIsTackleAble(true);
        monster.SetIsKnockBackAble(false);
        monster.SetIsTackleAble(true);
    }

    public void SetAfterSkill()
    {
        SetMaxAttackCoolTime();

        monster.SetIsFixedAnimation(false);
        monster.SetIsTackleAble(false);
        monster.SetIsKnockBackAble(true);
        monster.spriteRenderer.transform.DORotate(Vector3.zero, 0.1f);

        monster.SetStatus(MonsterStatus.Idle);
    }
}