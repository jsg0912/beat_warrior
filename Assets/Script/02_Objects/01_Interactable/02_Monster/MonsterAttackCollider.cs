using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterAttackCollider : MonoBehaviour
{
    protected Monster monster;
    public virtual void Initiate(Monster monster)
    {
        this.monster = monster;
    }
    public abstract void OnTriggerEnter2D(Collider2D other);
    protected int PlayerDirection()
    {
        return Player.Instance.transform.position.y > monster.transform.position.y ? 1 : -1;
    }
}
