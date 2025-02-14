using UnityEngine;
using System.Collections;

public class SoulObject : MonoBehaviour
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
            SoundManager.Instance.SFXPlay("soulGet", SoundList.Instance.soulGet);
            Inventory.Instance.ChangeSoulNumber(ObjectConstant.SoulIdleMotionSpeed);
            Destroy(gameObject);
        }
    }
}
