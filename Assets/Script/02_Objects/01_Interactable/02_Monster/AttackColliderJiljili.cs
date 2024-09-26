using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderJiljili : MonsterAttackCollider
{
    public override void Initiate(Monster monster)
    {
        base.Initiate(monster);
        GetComponent<Rigidbody2D>().velocity = new Vector3(GetPlayerDirection() * 5, 0, 0);
        //Destroy(this.gameObject, 5.0f);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Player"))
        {
            Player.Instance.GetDamaged(1);
            Destroy(this.gameObject);
        }
    }
}
