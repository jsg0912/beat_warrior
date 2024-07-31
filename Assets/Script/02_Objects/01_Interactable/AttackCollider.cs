using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private int atk;
    private float attackForce;

    void Start()
    {
        atk = PlayerSkillConstant.attackAtk;
        attackForce = PlayerSkillConstant.attackKnockbackRange;

        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (!obj.CompareTag("Monster")) return;

        obj.GetComponent<Monster>().GetDamaged(atk);

        int dir = Player.Instance.transform.position.x < obj.transform.position.x ? 1 : -1;
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * attackForce, 0.0f), ForceMode2D.Impulse);
    }
}
