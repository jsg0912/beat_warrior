using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected LayerMask GroundLayer;
    protected bool isEndOfGround;

    protected virtual bool IsMoveable() { return isEndOfGround == false; }
    protected int direction() { return monster.GetDirection(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }
}
