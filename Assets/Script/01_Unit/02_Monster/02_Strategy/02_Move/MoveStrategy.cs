using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected LayerMask GroundLayer;
    protected bool isEndOfGround;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        GroundLayer = LayerMask.GetMask(MonsterConstant.GroundLayer);
    }

    public override void PlayStrategy()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (IsMoveable() == false) return;

        monster.gameObject.transform.position += new Vector3(direction() * moveSpeed * Time.deltaTime, 0, 0);
        monster.IsWalking(true);
    }

    protected virtual bool IsMoveable() { return true; }
    protected int direction() { return monster.GetDirection(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }
}
