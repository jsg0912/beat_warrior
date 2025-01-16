using UnityEngine;

public abstract class Strategy
{
    protected Monster monster;
    protected LayerMask TargetLayer;
    protected BoxCollider2D collider;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;
        TargetLayer = LayerMask.GetMask(LayerConstant.Player);
        collider = monster.gameObject.GetComponent<BoxCollider2D>();
    }

    public virtual bool PlayStrategy() { return false; } // it returns whether the strategy plays or not.

    protected Vector3 GetPlayerPos() { return Player.Instance.transform.position; }
    protected Vector3 GetMonsterPos() { return monster.gameObject.transform.position; }
    protected Vector2 GetMonsterSize() { return new Vector2(collider.size.x, collider.size.y); }
    protected Vector3 GetMonsterMiddlePos() { return GetMonsterPos() + new Vector3(collider.offset.x, collider.offset.y, 0); }
    protected Vector3 GetMonsterBottomPos() { return GetMonsterPos() + new Vector3(collider.offset.x, collider.offset.y - collider.size.y / 2, 0); }
    protected int GetDirection() { return monster.GetDirection(); }
    protected int GetPlayerDirection() { return Player.Instance.transform.position.x > monster.transform.position.x ? 1 : -1; }
}