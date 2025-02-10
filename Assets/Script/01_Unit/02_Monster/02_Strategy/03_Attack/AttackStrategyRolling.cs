using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AttackStrategyRolling : AttackStrategy
{
    public float jumpPower;
    public int numJumps;
    public float duration;

    public AttackStrategyRolling(float jumpPower, int numJumps, float duration)
    {
        this.jumpPower = jumpPower;
        this.numJumps = numJumps;
        this.duration = duration;
    }

    protected override void SkillMethod()
    {
        Vector3 target = Player.Instance.transform.position;
        target.y = monster.transform.position.y + jumpPower;
        SetBeforeSkill();

        monster.transform.DOMoveX(target.x, duration)
            .SetEase(Ease.InSine);
        monster.transform.DOMoveY(target.y, duration)
            .SetEase(Ease.OutSine);
    }

    private void SetBeforeSkill()
    {
        monster.SetIsTackleAble(true);
        monster.SetIsKnockBackAble(false);
        monster.SetIsTackleAble(true);
    }

    public override void AttackEnd()
    {
        base.AttackEnd();

        monster.SetIsFixedAnimation(false);
        monster.SetIsTackleAble(false);
        monster.SetIsKnockBackAble(true);
        monster.spriteRenderer.transform.DORotate(Vector3.zero, 0.1f);
    }
}