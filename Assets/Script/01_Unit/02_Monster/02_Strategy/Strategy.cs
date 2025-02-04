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

    protected Vector3 GetPlayerPos() { return Player.Instance.GetBottomPos(); }
    protected Vector3 GetMonsterPos() { return monster.GetBottomPos(); }
    protected Vector3 GetMonsterMiddlePos() { return monster.GetMiddlePos(); }
    protected Vector3 GetMonsterFrontPos() { return GetMonsterPos() + new Vector3(GetMonsterSize().x * GetMovingDirectionFloat() / 2, 0, 0); }
    protected Vector3 GetMonsterMiddleFrontPos() { return GetMonsterFrontPos() + new Vector3(0, GetMonsterSize().y / 2, 0); }
    protected Vector2 GetMonsterSize() { return monster.GetSize(); }
    protected Direction GetMovingDirection() { return monster.GetMovingDirection(); }
    protected float GetMovingDirectionFloat() { return monster.GetMovingDirectionFloat(); }
}