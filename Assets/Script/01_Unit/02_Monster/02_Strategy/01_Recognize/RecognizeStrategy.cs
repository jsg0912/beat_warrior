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

    protected void StartChase()
    {
        if (monster.GetStatus() == MonsterStatus.Idle)
        {
            // DebugConsole.Log("StartChase");
            monster.SetStatus(MonsterStatus.Chase);
        }
    }
    protected void ReleaseChase()
    {
        if (monster.GetStatus() == MonsterStatus.Chase)
        {
            // DebugConsole.Log("ReleaseChase");
            monster.SetStatus(MonsterStatus.Idle);
        }
    }
}
