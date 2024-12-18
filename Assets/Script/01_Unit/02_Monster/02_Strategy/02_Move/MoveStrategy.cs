using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected LayerMask GroundLayer;
    protected bool isEndOfGround;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
    }

    public override void PlayStrategy()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (IsMoveable() == false) return;

        monster.gameObject.transform.position += new Vector3(GetDirection() * moveSpeed * Time.deltaTime, 0, 0);
        monster.SetIsWalking(true);
    }

    protected virtual bool IsMoveable() { return monster.GetIsMoveable(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }
}
