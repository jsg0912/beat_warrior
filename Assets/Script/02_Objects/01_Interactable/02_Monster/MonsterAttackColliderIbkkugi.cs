using UnityEngine;

public class MonsterAttackColliderIbkkugi : MonsterAttackCollider
{
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(damage, GetRelativeDirectionToPlayer());
            Destroy(gameObject);
        }
    }
}
