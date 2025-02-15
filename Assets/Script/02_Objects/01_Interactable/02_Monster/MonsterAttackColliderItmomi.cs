using UnityEngine;
using System.Collections;

public class MonsterAttackColliderItmomi : MonsterAttackCollider
{
    [SerializeField] private GameObject Warning;
    [SerializeField] private GameObject Thorn;

    void Start()
    {
        StartCoroutine(DestroyCreate(3.0f));
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player)) Player.Instance.GetDamaged(damage, GetRelativeDirectionToPlayer());
    }

    public IEnumerator DestroyCreate(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        MyPooler.ObjectPooler.Instance.ReturnToPool(PoolTag.ItmomiThrow, this.gameObject);
    }
}
