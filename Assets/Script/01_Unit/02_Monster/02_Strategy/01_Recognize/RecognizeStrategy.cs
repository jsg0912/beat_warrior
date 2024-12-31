public abstract class RecognizeStrategy : Strategy
{
    protected float recognizeRange;

    public override void PlayStrategy()
    {
        if (IsLookingTarget() == true) CheckTarget();
    }

    protected virtual bool IsLookingTarget() { return GetDirection() == GetPlayerDirection(); }
    protected abstract void CheckTarget();

    protected void StartChase()
    {
        if (monster.GetStatus() == MonsterStatus.Normal)
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
            monster.SetStatus(MonsterStatus.Normal);
        }
    }
}
