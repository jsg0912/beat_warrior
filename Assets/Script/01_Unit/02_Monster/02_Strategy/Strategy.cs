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

    public virtual void PlayStrategy() { }

    protected Vector3 GetPlayerPos() { return Player.Instance.transform.position; }
    protected Vector3 GetMonsterPos() { return monster.gameObject.transform.position; }
    protected int GetDirection() { return monster.GetDirection(); }
}