using UnityEngine;

public abstract class Pattern
{
    protected GameObject gameObject;
    public virtual void Initialize(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    public abstract void PlayPattern();
}