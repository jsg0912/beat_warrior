using System;

public abstract class RecognizeStrategy : Strategy
{
    protected float recognizeRange;

    public override bool PlayStrategy(Action callback = null) // return true if it is looking at the target.
    {
        if (monster.GetStatus() == MonsterStatus.Groggy) return false;
        if (monster.GetIsRecognizing() || IsLookingTarget()) return CheckTarget();
        return false;
    }

    protected virtual bool IsLookingTarget() { return GetMovingDirection() == monster.GetRelativeDirectionToPlayer(); }
    protected void TryFlipToTargetDirection() { if (!monster.GetIsAttacking() && !IsLookingTarget()) monster.FlipDirection(); }
    protected abstract bool CheckTarget();

    protected void TrySetChaseStatus() { if (!monster.GetIsRecognizing()) monster.SetStatus(MonsterStatus.Chase); }
    protected void ReleaseChase() { if (monster.GetStatus() == MonsterStatus.Chase) monster.SetStatus(MonsterStatus.Idle); }
}
