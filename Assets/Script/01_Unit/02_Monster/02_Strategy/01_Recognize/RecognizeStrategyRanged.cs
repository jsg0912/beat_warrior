using UnityEngine;

public class RecognizeStrategyRanged : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TargetLayer = LayerMask.GetMask(LayerConstant.Player);
        recognizeRange = MonsterConstant.RangedRecognizeRange;
    }

    protected override bool CheckTarget()
    {
        if (Vector2.Distance(GetPlayerPos(), GetMonsterPos()) < recognizeRange)
        {
            TrySetChaseStatus();
            return true;
        }
        else
        {
            ReleaseChase();
            return false;
        }
    }
}
