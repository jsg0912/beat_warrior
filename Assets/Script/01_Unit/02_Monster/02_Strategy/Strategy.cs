using UnityEngine;

public abstract class Strategy
{
    protected Monster monster;
    protected LayerMask TargetLayer;
    protected LayerMask GroundLayer;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;

        TargetLayer = LayerMask.GetMask(LayerConstant.Player);
        GroundLayer = LayerMask.GetMask(LayerConstant.Tile);
    }

    public virtual bool PlayStrategy() { return false; } // it returns whether the strategy plays or not.

    protected Vector3 GetPlayerPos() { return Player.Instance.GetBottomPos(); }
    protected Vector3 GetMonsterPos() { return monster.GetBottomPos(); }
    protected Vector3 GetMonsterMiddlePos() { return monster.GetMiddlePos(); }
    protected Vector3 GetMonsterFrontPos() { return GetMonsterPos() + new Vector3(GetMonsterSize().x * GetMovingDirectionFloat() / 2, 0, 0); }
    protected Vector3 GetMonsterMiddleFrontPos() { return GetMonsterFrontPos() + new Vector3(0, GetMonsterSize().y / 2, 0); }
    protected Vector2 GetMonsterSize() { return monster.GetSize(); }
    protected Direction GetMovingDirection() { return monster.GetMovingDirection(); }
    protected float GetMovingDirectionFloat() { return monster.GetMovingDirectionFloat(); }

    protected bool CheckWall()
    {
        float movingDirection = GetMovingDirectionFloat();
        Vector3 start = GetMonsterMiddleFrontPos();
        Vector3 dir = Vector3.right * movingDirection;

        RaycastHit2D rayHit = Physics2D.Raycast(start, dir, MonsterConstant.WallCheckRayDistance, LayerMask.GetMask(LayerConstant.Tile));
        // Debug.DrawLine(start, start + dir * MonsterConstant.WallCheckDistance, Color.red);
        return rayHit.collider != null && rayHit.collider.CompareTag(TagConstant.Base);
    }

    protected bool CheckEndOfGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterFrontPos() + new Vector3(0, 0.05f, 0), Vector3.down, MonsterConstant.GroundCheckRayDistance, GroundLayer);
        //Debug.DrawLine(GetMonsterFrontPos(), GetMonsterFrontPos() + Vector3.down * MonsterConstant.GroundCheckRayDistance, Color.red);

        return rayHit.collider == null;
    }

    protected bool CheckGround()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(GetMonsterPos() + new Vector3(0, 0.05f, 0), Vector3.down, MonsterConstant.GroundCheckRayDistance, GroundLayer);
        //Debug.DrawLine(GetMonsterFrontPos(), GetMonsterFrontPos() + Vector3.down * MonsterConstant.GroundCheckRayDistance, Color.red);

        return rayHit.collider != null;
    }
}