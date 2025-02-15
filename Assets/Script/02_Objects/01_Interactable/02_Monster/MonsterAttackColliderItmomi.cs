using UnityEngine;
using System.Collections;

public class MonsterAttackColliderItmomi : MonsterAttackCollider
{
    [SerializeField] private GameObject Warning;
    [SerializeField] private GameObject Thorn;

    public override void Initiate()
    {
        // StartCoroutine(enumerator());
    }

    // private IEnumerator enumerator()
    // {
    //     yield return new WaitForSeconds(1.0f);
    //     Util.SetActive(Warning, false);
    //     Util.SetActive(Thorn, true);
    //     yield return new WaitForSeconds(3.0f);
    //     Destroy(gameObject);
    // }
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
