using UnityEngine;

public class AttackStrategyThrowItmomi : AttackStrategyCreate
{
    protected override void SkillMethod()
    {
        SetAttackDirection();
        base.SkillMethod();
        if (attackDirection == Direction.Right) Util.FlipLocalScaleX(obj);
        obj.transform.position = new Vector3(GetPlayerPos().x, GetMonsterPos().y, 0);
    }
}