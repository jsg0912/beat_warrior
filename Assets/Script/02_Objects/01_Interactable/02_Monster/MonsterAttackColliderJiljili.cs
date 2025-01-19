using UnityEngine;

public class MonsterAttackColliderJiljili : MonsterAttackCollider
{
    public override void Initiate()
    {
        base.Initiate();
        /*
            Vector3 offset = new Vector3(0, MonsterConstant.ThrowObjectYOffset, 0);
            transform.position = monster.transform.position + offset;

            GetComponent<Rigidbody2D>().velocity
                = new Vector3(GetPlayerDirection(), 0, 0) * MonsterConstant.AttackThrowSpeed[monster.monsterName];
            Destroy(this.gameObject, 5.0f);
        */
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(GetMonsterAtk());
            Destroy(this.gameObject);
        }
    }
}
