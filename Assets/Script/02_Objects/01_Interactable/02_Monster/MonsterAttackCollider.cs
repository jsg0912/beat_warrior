using UnityEngine;

public abstract class MonsterAttackCollider : PhysicalObject
{
    [SerializeField] protected Monster monster;
    [SerializeField] public Rigidbody2D rb;

    public virtual void Initiate() { }
    protected int GetMonsterAtk() { return monster.GetCurrentStat(StatKind.ATK); }
    public abstract void OnTriggerEnter2D(Collider2D other);
}
