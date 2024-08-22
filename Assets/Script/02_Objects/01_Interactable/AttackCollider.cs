using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private bool knockBack;

    protected int atk;
    private float attackForce;
    protected List<GameObject> TargetMonster;

    void Start()
    {
        attackForce = PlayerSkillConstant.attackKnockBackRange;

        TargetMonster = new List<GameObject>();
        Destroy(gameObject, 0.1f);
    }

    public void SetAtk(int atk)
    {
        this.atk = atk;
    }

    private void KnockBack(GameObject obj)
    {
        int dir = Player.Instance.transform.position.x < obj.transform.position.x ? 1 : -1;
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * attackForce, 0.0f), ForceMode2D.Impulse);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (!obj.CompareTag("Monster")) return;

        if (TargetMonster.Contains(obj)) return;

        if (knockBack) KnockBack(obj);

        obj.GetComponent<Monster>().GetDamaged(atk);
        TargetMonster.Add(obj);
    }
}
