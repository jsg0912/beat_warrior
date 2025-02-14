using UnityEngine;

public class AttackStrategyThrowItmomi : AttackStrategyCreate
{
    private float offsetX = 1.0f;

    protected override void AttackMethod()
    {
        base.AttackMethod();
        if (attackDirection == Direction.Right) Util.FlipLocalScaleX(obj);
        obj.transform.position = ClosestGround();
    }

    protected Vector3 ClosestGround()
    {
        Vector3 offset = new Vector3(Player.Instance.GetMovingDirectionFloat() * offsetX, 0, 0);
        RaycastHit2D rayHit = Physics2D.Raycast(GetPlayerPos() + offset, Vector3.down, 5f, LayerMask.GetMask(LayerConstant.Tile));

        if (rayHit.collider == null) return Physics2D.Raycast(GetPlayerPos() + offset + new Vector3(0, 5, 0), Vector3.down, 5f, LayerMask.GetMask(LayerConstant.Tile)).point;
        return rayHit.point;
    }
}