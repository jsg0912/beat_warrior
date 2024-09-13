using System.Collections;
using UnityEngine;

public class RecognizeRangedMonster : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        RecognizeRange = MonsterConstant.RangedRecognizeRange;
        TargetLayer = LayerMask.GetMask(MonsterConstant.PlayerLayer);
    }

    public override void PlayStrategy()
    {
        CheckTarget();
    }

    private void CheckTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(CurrentPos(), RecognizeRange, TargetLayer);

        if (collider == null) return;

        if (collider.transform.position.x > CurrentPos().x) monster.SetDirection(Direction.Right);
        else monster.SetDirection(Direction.Left);
    }
}
