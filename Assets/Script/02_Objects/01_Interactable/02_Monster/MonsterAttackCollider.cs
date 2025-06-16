using UnityEngine;

public abstract class MonsterAttackCollider : PhysicalObject
{
    [SerializeField] public Rigidbody2D rb;
    protected int damage;

    public virtual void Initialize() { }
    public void SetMonsterAtk(int damage) { this.damage = damage; }
    public abstract void OnTriggerEnter2D(Collider2D other);
    protected Direction GetRelativeDirectionToPlayer() { return Player.Instance.GetRelativeDirectionToTarget(transform.position); }
}
