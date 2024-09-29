public abstract class RecognizeStrategy : Strategy
{
    protected float recognizeRange;

    public override void PlayStrategy()
    {
        if (IsLookingTarget() == true) CheckTarget();
    }

    protected virtual bool IsLookingTarget() { return GetDirection() * (GetPlayerPos().x - GetMonsterPos().x) > 0; }
    protected abstract void CheckTarget();
    protected void ReleaseChase()
    {
        if (monster.GetStatus() == MonsterStatus.Chase) monster.SetStatus(MonsterStatus.Normal);
    }
}
