using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderIbkkugi : MonsterAttackCollider
{
    public override void Initiate(Monster monster)
    {
        base.Initiate(monster);
        GetComponent<Rigidbody2D>().AddForce(new Vector3(PlayerDirection() * 5, 5, 0), ForceMode2D.Impulse);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Player"))
        {
            Player.Instance.GetDamaged(1);
            Destroy(this.gameObject);
        }

        if (obj.CompareTag("Base") || obj.CompareTag("Tile")) Destroy(this.gameObject);
    }
}
