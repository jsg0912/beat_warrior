using System.Collections;
using UnityEngine;

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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(1);
            Destroy(gameObject);
        }
    }
}
