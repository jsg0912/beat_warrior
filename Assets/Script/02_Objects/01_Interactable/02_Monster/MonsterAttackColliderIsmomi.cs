using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MonsterAttackColliderIsmomi : MonsterAttackCollider
{
    [SerializeField] private GameObject Warning;
    [SerializeField] private GameObject Thorn;

    public override void Initiate()
    {
        StartCoroutine(enumerator());
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(1.0f);
        Util.SetActive(Warning, false);
        Util.SetActive(Thorn, true);
        Thorn.transform.DOMove(Thorn.transform.position + new Vector3(0, 0.5f, 0), 0.3f);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player)) Player.Instance.GetDamaged(damage);
    }
}
