using Unity.Mathematics;
using UnityEngine;

public abstract class Strategy
{
    protected Monster monster;
    protected LayerMask TargetLayer;
    // TODO: Box인지 Poly인지를 담고 있는 하나의 MonsterCollider와 같은 Class가 필요함 - SDH, 20250119
    protected BoxCollider2D colliderBox;
    protected PolygonCollider2D colliderPoly;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;
        TargetLayer = LayerMask.GetMask(LayerConstant.Player);
        colliderBox = monster.gameObject.GetComponent<BoxCollider2D>();
        colliderPoly = monster.gameObject.GetComponent<PolygonCollider2D>();
    }

    public virtual bool PlayStrategy() { return false; } // it returns whether the strategy plays or not.

    protected Vector3 GetPlayerPos() { return Player.Instance.transform.position; }
    protected Vector3 GetMonsterPos() { return monster.gameObject.transform.position; }
    protected Vector2 GetMonsterSize()
    {
        if (colliderBox != null) return Util.GetSizeBoxCollider2D(colliderBox);
        else if (colliderPoly != null) return Util.GetSizePolygonCollider2D(colliderPoly);
        else return Vector2.zero;
    }
    protected Vector3 GetMonsterMiddlePos()
    {
        if (colliderBox != null) return Util.GetMiddlePosBoxCollider2D(colliderBox);
        else if (colliderPoly != null) return Util.GetMiddlePosPolygonCollider2D(colliderPoly);
        else return Vector3.zero;
    }
    protected Vector3 GetMonsterBottomPos()
    {
        if (colliderBox != null) return Util.GetBottomPosBoxCollider2D(colliderBox);
        else if (colliderPoly != null) return Util.GetBottomPosPolygonCollider2D(colliderPoly);
        else return Vector3.zero;
    }
    protected Direction GetMovingDirection() { return monster.GetMovingDirection(); }
    protected float GetMovingDirectionFloat() { return monster.GetMovingDirectionFloat(); }
    protected Direction GetRelativePlayerDirection() { return Player.Instance.transform.position.x > monster.transform.position.x ? Direction.Right : Direction.Left; }
    protected float GetRelativePlayerDirectionFloat() { return Player.Instance.transform.position.x > monster.transform.position.x ? 1.0f : -1.0f; }
}