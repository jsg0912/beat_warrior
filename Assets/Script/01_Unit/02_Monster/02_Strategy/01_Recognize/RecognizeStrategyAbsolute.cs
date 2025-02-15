using System;

public class RecognizeStrategyAbsolute : RecognizeStrategy
{
    public override bool PlayStrategy(Action callback = null) // return true if it is looking at the target.
    {
        if (monster.GetStatus() == MonsterStatus.Groggy) return false;
        return CheckTarget();
    }

    protected override bool CheckTarget()
    {
        return true;
    }
}