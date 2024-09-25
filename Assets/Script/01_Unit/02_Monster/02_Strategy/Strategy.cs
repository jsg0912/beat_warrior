using UnityEngine;

public abstract class Strategy
{
    protected Monster monster;
    protected LayerMask TargetLayer;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;
        TargetLayer = LayerMask.GetMask(MonsterConstant.PlayerLayer);
    }

    public virtual void PlayStrategy() { }

    protected Vector3 PlayerPos() { return Player.Instance.transform.position; }
    protected Vector3 CurrentPos() { return monster.gameObject.transform.position; }
    protected int direction() { return monster.GetDirection(); }
}