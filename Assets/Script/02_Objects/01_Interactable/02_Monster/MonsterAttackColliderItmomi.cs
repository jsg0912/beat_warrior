using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MonsterAttackColliderItmomi : MonsterAttackCollider
{
    [SerializeField] private GameObject Warning;
    [SerializeField] private GameObject Thorn;

    public override void Initiate()
    {
        StartCoroutine(enumerator());
    }

    private IEnumerator enumerator()
    {
        Debug.Log("nani");
        yield return new WaitForSeconds(2.0f);
        Util.SetActive(Warning, false);
        Debug.Log("warning turn off");
        Util.SetActive(Thorn, true);
        // Thorn.transform.DOMove(Thorn.transform.position + new Vector3(0, 0.5f, 0), 0.3f);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player)) Player.Instance.GetDamaged(damage);
    }
}
