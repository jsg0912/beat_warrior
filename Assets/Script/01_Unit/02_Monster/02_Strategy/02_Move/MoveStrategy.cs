using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected LayerMask GroundLayer;
    protected bool isEndOfGround;
    private const float GROUNDCHECKRAY = 0.1f;

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
        if (!IsMoveable())
        {
            monster.SetWalkingAnimation(false);
            return false;
        }

        CheckWall();

        monster.gameObject.transform.position += new Vector3(GetMovingDirectionFloat() * moveSpeed * Time.deltaTime, 0, 0);
        monster.SetWalkingAnimation(true);
        return true;
    }
    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterFrontPos(), Vector3.down, GROUNDCHECKRAY, GroundLayer);
        //Debug.DrawLine(GetMonsterFrontPos(), GetMonsterFrontPos() + Vector3.down * GROUNDCHECKRAY, Color.red);

        if (rayHit.collider == null) isEndOfGround = true;
        else isEndOfGround = false;
    }
    protected virtual void CheckWall()
    {
        float movingDirection = GetMovingDirectionFloat();
        Vector3 start = GetMonsterMiddleFrontPos();
        Vector3 dir = Vector3.right * movingDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, 0.1f, LayerMask.GetMask(LayerConstant.Tile));
        Debug.DrawLine(start, start + dir * 0.1f, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag("Base")) FlipDirection();
    }
    protected virtual bool IsMoveable() { return monster.GetIsMoveable(); }
    protected void SetMovingDirection(Direction direction) { monster.SetMovingDirection(direction); }
    protected void FlipDirection() { monster.FlipDirection(); }
    protected virtual Vector3 GetRayStartPoint() { return GetMonsterPos(); }
}
