// Fix인 몬스터가 방향만 바꾸는 전략 - 만약 Fix이고 진짜 방향도 안바꾸는 전략이 필요하면 Naming 수정해야함 - SDH, 20250208
public class MoveStrategyFix : MoveStrategy
{
    public override bool PlayStrategy()
    {
        TryFlipDirection();
        return true;
    }

    protected override bool TryMove()
    {
        return false;
    }
}
