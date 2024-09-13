using System.Collections;
using UnityEngine;

public class RecognizeRangedMonster : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        RecognizeRange = MonsterConstant.RangedRecognizeRange;
    }

    public override void PlayStrategy()
    {
        CheckTarget();
    }

    private void CheckTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(CurrentPos(), RecognizeRange, TargetLayer);

        if (collider == null)
        {
            if (monster.GetStatus() == MonsterStatus.Chase) monster.SetStatus(MonsterStatus.Normal);
            return;
        }

        monster.SetStatus(MonsterStatus.Chase);
    }
}
