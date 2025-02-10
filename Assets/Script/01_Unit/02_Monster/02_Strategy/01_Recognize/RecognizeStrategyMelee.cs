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
        if (monster.isChasing)
        {
            if (Vector2.Distance(GetPlayerPos(), GetMonsterPos()) > recognizeRange) ReleaseChase();
            else ChaseTarget();
            return;
        }

        Vector3 dir = Vector3.right * GetMovingDirectionFloat();
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterMiddlePos(), dir, recognizeRange, TargetLayer);
        Debug.DrawLine(GetMonsterMiddlePos(), GetMonsterMiddlePos() + dir * recognizeRange, Color.red);

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
