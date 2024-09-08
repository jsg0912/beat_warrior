using UnityEngine;

public abstract class Pattern
{
    protected Animator anim;
    protected GameObject gameObject;
    protected Monster monster;
    protected Direction direction;
    protected float moveSpeed;

    public virtual void Initialize(GameObject gameObject)
    {
        this.gameObject = gameObject;
        monster = gameObject.GetComponent<Monster>();
    }

    protected void Move()
    {
        if (IsMoveable() == false) return;

        monster.SetDirection(direction);
        gameObject.transform.position += new Vector3((int)direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    protected virtual bool IsMoveable() { return false; }

    public abstract void PlayPattern();

    protected void ChangeDirection()
    {
        direction = (Direction)(-1 * (int)direction);
    }

    public abstract Pattern Copy();
}