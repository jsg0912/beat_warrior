using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AttackStrategyRolling : AttackStrategy
{
    public float jumpPower = 3f; // 점프 높이
    public int numJumps = 1;  // 튀는 횟수
    public float duration = 1f; // 전체 애니메이션 시간

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
        float rotateAngle = monster.GetRelativePlayerDirectionFloat() * -1980 * numJumps;

        Vector3 target = Player.Instance.transform.position;
        target.y = monster.transform.position.y + jumpPower;
        SetBeforeSkill();

        monster.transform.DOJump(target, jumpPower, numJumps, duration)
            .SetEase(Ease.OutQuad); // 부드러운 튀는 느낌

        //monster.spriteRenderer.transform.DORotate(new Vector3(0, 0, rotateAngle), duration, RotateMode.FastBeyond360)
        //.SetEase(Ease.Linear); // 일정한 속도로 회전
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