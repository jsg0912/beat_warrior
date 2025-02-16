using System;

public class RecognizeStrategyAbsolute : RecognizeStrategy
{
    public override bool PlayStrategy(Action callback = null) // return true if it is looking at the target.
    {
        if (monster.GetStatus() == MonsterStatus.Groggy) return false;
        bool success = CheckTarget();
        if (success) TrySetChaseStatus();
        return success;
    }

    protected override bool CheckTarget()
    {
        return true;
    }
}