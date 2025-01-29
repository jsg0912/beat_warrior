using UnityEngine;

public abstract class MonsterAttackCollider : PhysicalObject
{
    [SerializeField] public Rigidbody2D rb;
    protected int damage;

    public virtual void Initiate() { }
    public void SetMonsterAtk(int damage) { this.damage = damage; }
    public abstract void OnTriggerEnter2D(Collider2D other);
}
