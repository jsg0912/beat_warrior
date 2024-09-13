using UnityEngine;

public class RecognizeRanged : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        recognizeRange = MonsterConstant.RangedRecognizeRange;
    }

    protected override void CheckTarget()
    {
        if (Vector3.Distance(PlayerPos(), CurrentPos()) < recognizeRange)
        {
            monster.SetStatus(MonsterStatus.Chase);
            return;
        }

        ReleaseChase();
    }
}
