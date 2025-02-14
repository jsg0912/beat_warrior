using DG.Tweening;
using UnityEngine;

// TODO: Naming 수정 및 거의 덜덜이 전용이기 떄문에 Common화 필요
public class AttackStrategyRolling : AttackStrategySplash
{
    public float jumpPower;
    public float duration;

    public AttackStrategyRolling(float jumpPower, float duration, Vector2 splashRange) : base(splashRange)
    {
        this.jumpPower = jumpPower;
        this.duration = duration;
    }

    protected override bool TryAttack()
    {
        bool success = base.TryAttack();
        if (success) JumpAnimation();
        return success;
    }

    protected void JumpAnimation()
    {
        Vector3 target = GetPlayerPos();
        Vector3 origin = GetMonsterPos();
        target.y = origin.y + jumpPower;

        Rigidbody2D rb = monster.GetComponent<Rigidbody2D>();
        float gravity = rb.gravityScale; // TODO: Monster Common에 gravityScale 관리 함수 추가

        Sequence jumpSequence = DOTween.Sequence();

        // 덜덜이 튀어오르기
        jumpSequence.Append(monster.transform.DOMoveX(target.x, duration)
            .SetEase(Ease.InSine))
            .Join(monster.transform.DOMoveY(target.y, duration)
            .SetEase(Ease.OutSine))
            .AppendCallback(() =>
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            });

        // 덜덜이 공중에서 대기
        jumpSequence.AppendInterval(0.7f);

        // 덜덜이 내리 찍기
        jumpSequence.AppendCallback(() =>
        {
            origin = monster.GetBottomPos();
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, GroundLayer);

            monster.transform.DOMoveY(monster.transform.position.y - origin.y + hit.point.y, 0.3f) // transform의 position을 써야함...
                .SetEase(Ease.InQuad)
                .OnStart(() =>
                {
                    monster.SetIsFixedAnimation(false);
                    monster.PlayAnimation(MonsterConstant.fallAnimTrigger);
                    monster.SetIsFixedAnimation(true);
                })
                .OnComplete(() =>
                {
                    // 땅에 닿은 후 실행할 함수
                    rb.gravityScale = gravity;
                    monster.SetIsKnockBackAble(true);
                    monster.SetIsFixedAnimation(false);
                    monster.PlayAnimation(MonsterConstant.attackEndAnimTrigger);
                    SplashAttack();
                    monster.playAfterAttackEffect();
                });
        });
    }

    protected override void AttackMethod()
    {
        SetBeforeSkill();
    }

    private void SetBeforeSkill()
    {
        monster.SetIsKnockBackAble(false);
    }

    public override void AttackEnd()
    {
        monster.PlayAnimation(MonsterStatus.Groggy, true); // TODO: 실행 시점에 대해 고민 필요
        // 주의: base를 안쓰기 떄문에, base에서 공용으로 써야하는 것들이 새로 생기면, 여기도 추가해야할 수 있다.
        monster.SetIsKnockBackAble(true);
        monster.SetIsFixedAnimation(false);
        monster.pattern.PlayGroggy();
    }
}