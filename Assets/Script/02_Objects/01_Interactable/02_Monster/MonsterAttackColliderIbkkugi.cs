using UnityEngine;

public class MonsterAttackColliderIbkkugi : MonsterAttackCollider
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(GetMonsterAtk());
            Destroy(gameObject);
        }

        if (obj.CompareTag(TagConstant.Base) || obj.CompareTag(TagConstant.Tile)) Destroy(gameObject);
    }
}
