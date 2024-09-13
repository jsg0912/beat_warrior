using System.Collections;
using UnityEngine;

public class RecognizeRangedMonster : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        RecognizeRange = MonsterConstant.RangedRecognizeRange;
        TargetLayer = LayerMask.GetMask(MonsterConstant.PlayerLayer);
        alert = false;
    }

    public override void PlayStrategy()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(CurrentPos(), RecognizeRange, TargetLayer);

        foreach (Collider2D collider in collider2Ds)
        {
            alert = true;

            if (collider.transform.position.x > CurrentPos().x) monster.SetDirection(Direction.Right);
            else monster.SetDirection(Direction.Left);

        }
        if (alert == true)
        {
            if (Physics2D.OverlapCircle(CurrentPos(), RecognizeRange, TargetLayer) == false)
            {
                alert = false;
            }
        }
    }
}
