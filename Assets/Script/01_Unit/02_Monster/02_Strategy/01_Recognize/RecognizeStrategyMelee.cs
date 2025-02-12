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

        Vector3 start = GetMonsterPos() + new Vector3(0, Player.Instance.GetSize().y, 0) - Util.OffsetY;
        Vector3 dir = Vector3.right * GetMovingDirectionFloat();
        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, recognizeRange, TargetLayer);
        //Debug.DrawRay(start, dir * recognizeRange, Color.red);

        if (rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Player)) StartChase();
        else ReleaseChase();
    }
}
