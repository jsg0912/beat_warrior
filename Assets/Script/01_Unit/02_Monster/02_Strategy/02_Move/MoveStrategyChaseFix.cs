using System;

public class MoveStrategyChaseFix : MoveStrategyChase
{
    public override bool PlayStrategy(Action callback = null)
    {
        ChaseTarget();
        return true;
    }

    protected override bool TryMove()
    {
        return false;
    }
}
