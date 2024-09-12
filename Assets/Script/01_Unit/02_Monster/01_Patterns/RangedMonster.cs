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
        isMoveable = true;
    }

    public override void PlayStrategy()
    {
        CheckCollision();
    }

    protected bool GetIsMoveable()
    {
        return isMoveable;
    }

    private void CheckCollision()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(CurrentPos(), RecognizeRange, TargetLayer);

        foreach (Collider2D collider in collider2Ds)
        {
            alert = true;

            if (isMoveable == true)
            {
                if (collider.transform.position.x > CurrentPos().x) monster.SetDirection(Direction.Right);
                else monster.SetDirection(Direction.Left);
            }

        }
        if (alert == true)
        {
            if (Physics2D.OverlapCircle(CurrentPos(), RecognizeRange, TargetLayer) == false)
            {
                alert = false;
                isMoveable = true;
            }
        }
        RaycastHit2D rayHit = Physics2D.Raycast(CurrentPos() + new Vector3(monster.GetDirection(), 0, 0), Vector3.down, 1, LayerMask.GetMask("Tile"));
        if (rayHit.collider == null)
        {
            if (alert == true) isMoveable = false;
            else monster.ChangeDirection();
        }
    }
}
