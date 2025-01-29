using UnityEngine;

public class RecognizeStrategyRanged : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TargetLayer = LayerMask.GetMask(LayerConstant.Tile, LayerConstant.Player);
        recognizeRange = MonsterConstant.RangedRecognizeRange;
    }

    protected override void CheckTarget()
    {
        Vector3 dir = Vector3.right * GetMovingDirectionFloat();
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterMiddlePos(), dir, recognizeRange, TargetLayer);


        if (rayHit.collider != null)
        {
            if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer(LayerConstant.Tile))
            {
                ReleaseChase();
            }
            else if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer(LayerConstant.Player))
            {
                StartChase();
            }
        }
        else
        {
            ReleaseChase();
        }
    }
}
