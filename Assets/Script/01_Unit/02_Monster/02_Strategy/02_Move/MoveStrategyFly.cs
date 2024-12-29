using UnityEngine;

public class MoveStrategyFly : MoveStrategy
{
    protected Vector3 OriginPos;
    protected float moveRange;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        OriginPos = GetMonsterPos();
        moveRange = 3.0f;
        moveSpeed = 0;

        // 초기 방향 랜덤 설정
        SetDirection(Random.Range(0, 2) == 0 ? Direction.Right : Direction.Left);
    }

    public override void PlayStrategy()
    {
        base.PlayStrategy();

        CheckRange();
    }

    protected void CheckRange()
    {
        if (Vector3.Distance(OriginPos, GetMonsterPos()) > moveRange) ChangeDirection();
    }
}
