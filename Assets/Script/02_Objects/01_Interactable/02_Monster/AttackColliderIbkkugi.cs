using UnityEngine;

public class AttackColliderIbkkugi : MonsterAttackCollider
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(1);
            Destroy(gameObject);
        }

        if (obj.CompareTag(TagConstant.Base) || obj.CompareTag(TagConstant.Tile)) Destroy(gameObject);
    }
}
