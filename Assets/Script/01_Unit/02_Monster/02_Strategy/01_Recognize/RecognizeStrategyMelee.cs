using UnityEngine;

public class RecognizeStrategyMelee : RecognizeStrategy
{
    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        TargetLayer = LayerMask.GetMask(MonsterConstant.PlayerLayer, MonsterConstant.GroundLayer);
        recognizeRange = MonsterConstant.MeleeRecognizeRange;
    }

    protected override void CheckTarget()
    {
        Vector3 offset = new Vector3(0, 1.0f, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterPos() + offset, Vector3.right * GetDirection(), recognizeRange, TargetLayer);
        if (rayHit.collider != null)
        {
            if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer(MonsterConstant.GroundLayer))
            {
                ReleaseChase();
            }
            else if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer(MonsterConstant.PlayerLayer))
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
