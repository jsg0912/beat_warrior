using UnityEngine;

public abstract class MonsterAttackCollider : MonoBehaviour
{
    protected Monster monster;
    public virtual void Initiate(Monster monster)
    {
        this.monster = monster;
    }
    public abstract void OnTriggerEnter2D(Collider2D other);
    protected int GetPlayerDirection()
    {
        return Player.Instance.transform.position.x > monster.transform.position.x ? 1 : -1;
    }
}
