using UnityEngine;

public class AttackStrategyThrowItmomi : AttackStrategyCreate
{
    private float offsetX = 1.0f;

    protected override void SkillMethod()
    {
        base.SkillMethod();
        if (attackDirection == Direction.Right) Util.FlipLocalScaleX(obj);
        obj.transform.position = ClosestGround();
    }

    protected Vector3 ClosestGround()
    {
        Vector3 offset = new Vector3(Player.Instance.GetMovingDirectionFloat() * offsetX, 0, 0);
        return Physics2D.Raycast(GetPlayerPos() + offset, Vector3.down, 5f, LayerMask.GetMask(LayerConstant.Tile)).point;
    }
}