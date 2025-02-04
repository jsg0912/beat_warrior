using UnityEngine;

public class MonsterAttackColliderKoppulso : MonsterAttackCollider
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + " / " + other.gameObject.name);
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player)) Player.Instance.GetDamaged(damage, GetRelativeDirectionToPlayer());
    }
}
