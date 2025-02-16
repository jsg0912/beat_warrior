using UnityEngine;

public class BossTentacle : MonsterAttackCollider
{
    public Monster Boss;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player)) Player.Instance.GetDamaged(damage, GetRelativeDirectionToPlayer());
    }
}
