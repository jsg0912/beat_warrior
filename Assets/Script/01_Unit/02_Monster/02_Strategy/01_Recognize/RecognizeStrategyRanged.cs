using UnityEngine;

public class RecognizeStrategyRanged : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        recognizeRange = MonsterConstant.RangedRecognizeRange;
    }

    protected override void CheckTarget()
    {
        if (Vector3.Distance(GetPlayerPos(), GetMonsterPos()) < recognizeRange)
        {
            monster.SetStatus(MonsterStatus.Chase);
            return;
        }

        ReleaseChase();
    }
}
