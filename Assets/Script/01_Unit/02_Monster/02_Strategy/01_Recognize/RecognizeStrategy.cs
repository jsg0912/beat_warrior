public abstract class RecognizeStrategy : Strategy
{
    protected float recognizeRange;

    public override bool PlayStrategy()
    {
        if (IsLookingTarget() == true) CheckTarget();
        return true;
    }

    protected virtual bool IsLookingTarget() { return GetMovingDirection() == monster.GetRelativeDirectionToPlayer(); }
    protected abstract void CheckTarget();

    protected void StartChase() { if (!monster.isChasing) monster.isChasing = true; }
    protected void ReleaseChase() { if (monster.isChasing) monster.isChasing = false; }
}
