using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour
{
    bool isUsed = false; // 2명이 동시에 맞는 순간 버그를 처리하기 위해 어쩔 수 없는 Bool 변수 - SDH, 20240213
    private void Start()
    {
        StartCoroutine(DestroyMarker(PlayerSkillConstant.markerDuration));
    }
    private void OnEnable()
    {
        isUsed = false;
        StartCoroutine(DestroyMarker(PlayerSkillConstant.markerDuration));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }



    public void SetVelocity(Vector2 start, Vector2 end)
    {
        isUsed = false;
        Vector2 direction = (end - start).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * PlayerSkillConstant.markerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;
        GameObject obj = Util.GetMonsterGameObject(collision);

        if (obj != null && obj.CompareTag(TagConstant.Monster) && obj.GetComponent<Monster>().GetIsAlive())
        {
            isUsed = true;
            Player.Instance.SetTarget(obj);
            obj.GetComponent<Monster>().SetTarget();
            PlayerUIManager.Instance.SwapMarkAndDash(false);
            MyPooler.ObjectPooler.Instance.ReturnToPool(PoolTag.Mark, this.gameObject);
        }
    }

    public IEnumerator DestroyMarker(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        if (!isUsed)
        {
            isUsed = true;
            MyPooler.ObjectPooler.Instance.ReturnToPool(PoolTag.Mark, this.gameObject);
        }
    }
}
