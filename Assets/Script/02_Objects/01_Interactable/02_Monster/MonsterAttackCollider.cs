using UnityEngine;

public abstract class MonsterAttackCollider : PhysicalObject
{
    [SerializeField] public Rigidbody2D rb;

    public virtual void Initiate() { }
    public abstract void OnTriggerEnter2D(Collider2D other);
}
