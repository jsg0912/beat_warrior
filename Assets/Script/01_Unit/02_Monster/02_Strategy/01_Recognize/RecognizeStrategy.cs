using UnityEngine;

public abstract class RecognizeStrategy : Strategy
{
    protected float recognizeRange;

    public override void PlayStrategy()
    {
        if (isLookingTarget() == true) CheckTarget();
    }

    protected int direction() { return monster.GetDirection(); }
    protected virtual bool isLookingTarget() { return direction() * (PlayerPos().x - CurrentPos().x) > 0; }

    protected abstract void CheckTarget();
    protected void ReleaseChase()
    {
        if (monster.GetStatus() == MonsterStatus.Chase) monster.SetStatus(MonsterStatus.Normal);
    }
}
