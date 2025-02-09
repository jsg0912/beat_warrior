using UnityEngine;

public class MoveStrategy : Strategy
{
    protected float moveSpeed;

    protected LayerMask GroundLayer;
    protected bool isEndOfGround;

    protected float timeLimit => 3 + Random.value * 2; // TODO: [Code Review - KMJ] Constant화 해야함
    protected Timer changeDestTimer = new Timer();

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
        SetRandomFlipTimer();
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

    //[Code Review - KMJ] TODO: AttackStrategyRush에도 있는데, Wall을 Check해서 bool을 return하는 함수를 만들고, 그에 따라 필요한 행동은 Strategy에서 알아서 하도록 수정 필요 (CheckGround도 마찬가지) - Nights, 20250201
    protected virtual void CheckWall()
    {
        float movingDirection = GetMovingDirectionFloat();
        Vector3 start = GetMonsterMiddleFrontPos();
        Vector3 dir = Vector3.right * movingDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, MonsterConstant.WallCheckRayDistance, LayerMask.GetMask(LayerConstant.Tile));
        // Debug.DrawLine(start, start + dir * MonsterConstant.WallCheckDistance, Color.red);
        if (rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base)) FlipDirection();
    }

    protected void CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterFrontPos() + new Vector3(0, 0.05f, 0), Vector3.down, MonsterConstant.GroundCheckRayDistance, GroundLayer);
        //Debug.DrawLine(GetMonsterFrontPos(), GetMonsterFrontPos() + Vector3.down * MonsterConstant.GroundCheckRayDistance, Color.red);

        if (rayHit.collider == null) isEndOfGround = true;
        else isEndOfGround = false;
    }

    protected virtual bool IsMoveable() { return monster.GetIsMoveable(); }
    protected void SetMovingDirection(Direction direction) { monster.SetMovingDirection(direction); }
    protected void FlipDirection() { monster.FlipDirection(); }
    protected virtual Vector3 GetRayStartPoint() { return GetMonsterPos(); }

    protected void SetRandomFlipTimer() { changeDestTimer.Initialize(timeLimit); }

    protected virtual void TryFlipDirection()
    {
        if (changeDestTimer.Tick()) return;

        // TODO: [Code Review - KMJ] Constant화 해야함
        int dest = Random.Range(0, 3);
        if (dest == 0) FlipDirection();
        SetRandomFlipTimer();
    }
}
