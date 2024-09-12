using UnityEngine;

public abstract class Strategy
{
    protected Monster monster;

    public virtual void Initialize(Monster monster)
    {
        this.monster = monster;
    }

    public virtual void PlayStrategy() { }

    protected Vector3 CurrentPos() { return monster.gameObject.transform.position; }
}