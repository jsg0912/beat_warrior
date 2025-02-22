using UnityEngine;
using System.Collections;
using MyPooler;

public class SoulObject : MonoBehaviour, IPooledObject
{
    private float floatHeight = 0.2f; // 떠오르는 높이
    private float floatSpeed = 1f; // 떠오르는 속도

    private Vector3 startPosition;

    void Start()
    {
        // 시작 위치 저장
        startPosition = transform.position;
        StartCoroutine(Float());
    }

    public void DiscardToPool()
    {
        ObjectPooler.Instance.ReturnToPool(PoolTag.Soul, gameObject);
    }

    private IEnumerator Float()
    {
        while (true) // 무한 반복
        {
            // sine 함수를 사용하여 부드럽게 움직이기
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);

            // 프레임마다 잠시 대기
            yield return null; // 다음 프레임까지 대기
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagConstant.Player))
        {
            SoundManager.Instance.SFXPlay(SoundList.Instance.soulGet);
            Inventory.Instance.ChangeSoulNumber(ObjectConstant.SoulIdleMotionSpeed);
            MyPooler.ObjectPooler.Instance.ReturnToPool(PoolTag.Soul, this.gameObject);
        }
    }

    public void DestroySoul()
    {
        Destroy(gameObject);
    }
}
