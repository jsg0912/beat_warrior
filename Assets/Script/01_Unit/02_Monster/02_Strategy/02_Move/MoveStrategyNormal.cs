public class MoveStrategyNormal : MoveStrategyRandom
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        moveSpeed = MonsterConstant.MoveSpeed[monster.monsterName];

        // 초기 방향 랜덤 설정
        SetMovingDirection(RandomSystem.RandomBool(1 / 3) ? Direction.Right : Direction.Left);
    }
}
