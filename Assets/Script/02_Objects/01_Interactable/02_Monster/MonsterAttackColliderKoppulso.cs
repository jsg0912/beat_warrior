using System.Collections;
using UnityEngine;

public class MonsterAttackColliderKoppulso : MonsterAttackCollider
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(1);
        }
    }
}
