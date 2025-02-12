using UnityEngine;

public class RecognizeStrategyRanged : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TargetLayer = LayerMask.GetMask(LayerConstant.Player);
        recognizeRange = MonsterConstant.RangedRecognizeRange;
    }

    protected override void CheckTarget()
    {
        //Collider2D collider = Physics2D.OverlapCircle(GetMonsterMiddlePos(), recognizeRange, TargetLayer);

        if (Vector2.Distance(GetPlayerPos(), GetMonsterPos()) < recognizeRange) StartChase();
        else ReleaseChase();
    }
}
