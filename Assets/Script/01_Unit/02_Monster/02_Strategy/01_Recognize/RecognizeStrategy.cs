using UnityEngine;

public class RecognizeStrategy : Strategy
{
    protected float RecognizeRange;
    protected LayerMask TargetLayer;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        TargetLayer = LayerMask.GetMask(MonsterConstant.PlayerLayer);
    }
}
