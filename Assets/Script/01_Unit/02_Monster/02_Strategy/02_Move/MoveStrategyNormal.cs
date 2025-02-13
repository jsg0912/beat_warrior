using UnityEngine;

public class MoveStrategyNormal : MoveStrategyRandom
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        // 초기 방향 랜덤 설정
        SetMovingDirection(Random.Range(0, 2) == 0 ? Direction.Right : Direction.Left);
    }
}
