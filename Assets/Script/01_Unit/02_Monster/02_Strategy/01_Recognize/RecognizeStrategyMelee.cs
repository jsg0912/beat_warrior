using UnityEngine;

public class RecognizeStrategyMelee : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TargetLayer = LayerMask.GetMask(LayerConstant.Tile, LayerConstant.Player);
        recognizeRange = MonsterConstant.MeleeRecognizeRange;
    }

    protected override void CheckTarget()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterMiddlePos(), Vector3.right * GetDirection(), recognizeRange, TargetLayer);
        //Debug.DrawLine(GetMonsterMiddlePos(), GetMonsterMiddlePos() + Vector3.right * GetDirection() * recognizeRange, Color.red);

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
