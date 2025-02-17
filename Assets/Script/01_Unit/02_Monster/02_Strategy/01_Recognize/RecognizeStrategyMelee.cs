using UnityEngine;

public class RecognizeStrategyMelee : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TargetLayer = LayerMask.GetMask(LayerConstant.Tile, LayerConstant.Player);
        recognizeRange = MonsterConstant.MeleeRecognizeRange;
    }

    protected override bool CheckTarget()
    {
        bool success = false;
        if (monster.GetIsRecognizing())
        {
            if (Vector2.Distance(GetPlayerPos(), GetMonsterPos()) > recognizeRange)
            {
                ReleaseChase();
            }
            else
            {
                TryFlipToTargetDirection(); // Player가 몬스터를 지나쳐갔을 때, Monster가 Player를 바라보도록 방향을 바꿔줌
                success = true;
            }
        }
        else
        {
            if (TryRecognizePlayer())
            {
                TrySetChaseStatus();
            }
            success = true;
        }
        return success;
    }

    private bool TryRecognizePlayer()
    {
        Vector3 start = GetMonsterPos();
        start.y += Player.Instance.GetSize().y - VectorCalculator.OffsetY;

        Vector3 dir = Vector3.right * GetMovingDirectionFloat();
        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, recognizeRange, TargetLayer);
        //Debug.DrawRay(start, dir * recognizeRange, Color.red);

        return rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Player);
    }
}
