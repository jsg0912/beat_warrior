using UnityEngine;

public class MoveStrategyFly : MoveStrategyRandom
{
    protected Vector3 OriginPos;
    protected float moveRange;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        OriginPos = GetMonsterPos();
        moveRange = 3.0f;
        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        // 초기 방향 랜덤 설정
        SetMovingDirection(Random.Range(0, 2) == 0 ? Direction.Right : Direction.Left);
    }

    public override bool PlayStrategy()
    {
        base.PlayStrategy();

        CheckRange();
        return true;
    }

    protected void CheckRange()
    {
        if (Vector3.Distance(OriginPos, GetMonsterPos()) > moveRange) FlipDirection();
    }
}
