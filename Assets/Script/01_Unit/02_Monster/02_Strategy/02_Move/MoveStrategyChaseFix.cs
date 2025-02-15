public class MoveStrategyChaseFix : MoveStrategyChase
{
    public override bool PlayStrategy()
    {
        ChaseTarget();
        return true;
    }

    protected override bool TryMove()
    {
        return false;
    }
}
