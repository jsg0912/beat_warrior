using Unity.Mathematics;
using UnityEngine;

public abstract class Strategy
{
    protected Monster monster;
    protected LayerMask TargetLayer;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;
        TargetLayer = LayerMask.GetMask(LayerConstant.Player);
    }

    public virtual bool PlayStrategy() { return false; } // it returns whether the strategy plays or not.

    protected Vector3 GetPlayerPos() { return Player.Instance.transform.position; }
    protected Vector3 GetMonsterPos() { return monster.gameObject.transform.position; }
    protected Vector2 GetMonsterSize() { return monster.GetSize(); }
    protected Vector3 GetMonsterMiddlePos() { return monster.GetMiddlePos(); }
    protected Vector3 GetMonsterBottomPos() { return monster.GetBottomPos(); }
    protected Direction GetMovingDirection() { return monster.GetMovingDirection(); }
    protected float GetMovingDirectionFloat() { return monster.GetMovingDirectionFloat(); }
    protected Direction GetRelativeDirectionToPlayer() { return Player.Instance.transform.position.x > monster.transform.position.x ? Direction.Right : Direction.Left; }
    protected float GetRelativePlayerDirectionFloat() { return Player.Instance.transform.position.x > monster.transform.position.x ? 1.0f : -1.0f; }
}