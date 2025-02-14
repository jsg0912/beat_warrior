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

    protected override bool TrySkill()
    {
        if (attackCoolTime > 0) return false;
        if (!CheckTarget()) return false;

        SetAttackDirection();
        monster.PlayAnimation(MonsterStatus.Attack);
        JumpAnimation();

        return true;
    }

    protected void JumpAnimation()
    {
        Vector3 target = Player.Instance.transform.position;
        target.y = monster.transform.position.y + jumpPower;

        Rigidbody2D rb = monster.GetComponent<Rigidbody2D>();
        float gravity = rb.gravityScale;

        Sequence jumpSequence = DOTween.Sequence();

        jumpSequence.Append(monster.transform.DOMoveX(target.x, duration)
            .SetEase(Ease.InSine));
        jumpSequence.Join(monster.transform.DOMoveY(target.y, duration)
            .SetEase(Ease.OutSine));

        jumpSequence.AppendCallback(() =>
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            });

        jumpSequence.AppendInterval(0.7f);

        jumpSequence.AppendCallback(() =>
        {
            Vector2 origin = monster.GetBottomPos();
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, GroundLayer);

            monster.transform.DOMoveY(monster.transform.position.y - origin.y + hit.point.y, 0.3f)
                .SetEase(Ease.InQuad)
                .OnStart(() =>
                {
                    rb.gravityScale = gravity;
                    monster.SetIsFixedAnimation(false);
                    monster.PlayAnimation(MonsterStatus.Groggy, true);
                })
                .OnComplete(() =>
                {
                    // 땅에 닿은 후 실행할 함수
                    monster.SetIsTackleAble(false);
                    monster.SetIsKnockBackAble(true);
                });
        });

        /*monster.transform.DOMoveX(target.x, duration)
            .SetEase(Ease.InSine);
        monster.transform.DOMoveY(target.y, duration)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                Rigidbody2D rb = monster.GetComponent<Rigidbody2D>();
                float gravity = rb.gravityScale;

                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;

                DOVirtual.DelayedCall(1.0f, () =>
                {
                    rb.gravityScale = gravity;
                    rb.velocity = Vector2.down * gravity * 10;

                    monster.SetIsFixedAnimation(false);
                    monster.PlayAnimation(MonsterConstant.attackEndAnimTrigger);
                });
            });*/
        // TODO: DoTween으로 내리찍고 아래 주석된 코드들 콜백으로 실행
        // monster.SetIsTackleAble(false);
        // monster.SetIsKnockBackAble(true);
        // monster.PlayAnimation(MonsterStatus.Groggy, true);

    }

    protected override void SkillMethod()
    {
        SetBeforeSkill();
    }

    private void SetBeforeSkill()
    {
        monster.SetIsTackleAble(true);
        monster.SetIsKnockBackAble(false);
        monster.SetIsTackleAble(true);
    }
}