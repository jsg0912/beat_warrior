using UnityEngine;

public class AttackColliderIbkkugi : MonsterAttackCollider
{
    public override void Initiate(Monster monster)
    {
        base.Initiate(monster);

        Vector3 offset = new Vector3(0, MonsterConstant.ThrowObjectYOffset, 0);
        transform.position = monster.transform.position + offset;

        float gravity = Physics2D.gravity.y;
        // 포물선 운동 시간 공식
        float time = Mathf.Sqrt(-8 * MonsterConstant.IbkkugiMaxHeight / gravity);
        float distance = Player.Instance.transform.position.x - monster.transform.position.x - MonsterConstant.ThrowObjectYOffset * GetPlayerDirection();
        Vector3 velocity = new Vector3(distance / time, -gravity * time / 2, 0);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(1);
            Destroy(this.gameObject);
        }

        if (obj.CompareTag(TagConstant.Base) || obj.CompareTag(TagConstant.Tile)) Destroy(this.gameObject);
    }
}
