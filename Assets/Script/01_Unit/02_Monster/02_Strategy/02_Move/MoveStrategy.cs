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

    public override bool PlayStrategy()
    {
        return Move();
    }

    protected virtual bool Move()
    {
        if (IsMoveable() == false)
        {
            monster.SetIsWalking(false);
            return false;
        }

        CheckWall();

        monster.gameObject.transform.position += new Vector3(GetDirection() * moveSpeed * Time.deltaTime, 0, 0);
        monster.SetIsWalking(true);
        return true;
    }
    protected virtual void CheckWall()
    {
        Vector3 start = GetMonsterMiddlePos() + new Vector3(GetMonsterSize().x / 2, 0, 0) * GetDirection();
        Vector3 dir = Vector3.right * GetDirection();

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        //Debug.DrawLine(start, start + dir * 0.1f, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag("Base")) ChangeDirection();
    }
    protected virtual bool IsMoveable() { return monster.GetIsMoveable(); }
    protected void SetDirection(Direction direction) { monster.SetDirection(direction); }
    protected void ChangeDirection() { monster.ChangeDirection(); }
    protected virtual Vector3 GetRayStartPoint() { return GetMonsterPos(); }
}
