using UnityEngine;
using System.Collections;

public class PlayerLift : ObjectWithInteractionPrompt
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;
    public float autoDescentDelay = 5f;

    private bool isMoving = false;
    private Vector3 targetPosition;
    private bool leverIsOn = false;
    private Coroutine autoDescentCoroutine;

    public GameObject lever;

    public void ToggleLever()
    {
        leverIsOn = !leverIsOn;
        lever.transform.rotation = Quaternion.Euler(0, 0, leverIsOn ? -45f : 45f);
    }

    void Start()
    {
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    private IEnumerator MoveLift()
    {
        isMoving = true;
        SoundManager.Instance.SFXPlay(SoundList.Instance.elevator);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;

        // 리프트가 endPoint(위쪽)에 도달했을 때만 자동 내려가기 타이머 시작
        if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
        {
            StartAutoDescentTimer();
        }
    }

    private void StartAutoDescentTimer()
    {
        // 기존 타이머가 있다면 중지
        if (autoDescentCoroutine != null)
        {
            StopCoroutine(autoDescentCoroutine);
        }

        autoDescentCoroutine = StartCoroutine(AutoDescentTimer());
    }

    private IEnumerator AutoDescentTimer()
    {
        yield return new WaitForSeconds(autoDescentDelay);

        // 10초 후에도 여전히 위에 있고 움직이지 않는 상태라면 자동으로 내려가기
        if (!isMoving && Vector3.Distance(transform.position, endPoint.position) < 0.1f)
        {
            targetPosition = startPoint.position;
            ToggleLever();
            StartCoroutine(MoveLift());
        }
    }

    public bool ActivateLift()
    {
        if (!isMoving)
        {
            // 자동 내려가기 타이머 중지 (수동으로 조작할 때)
            if (autoDescentCoroutine != null)
            {
                StopCoroutine(autoDescentCoroutine);
                autoDescentCoroutine = null;
            }

            if (Vector3.Distance(transform.position, startPoint.position) < 0.1f)
            {
                targetPosition = endPoint.position;
            }
            else
            {
                targetPosition = startPoint.position;
            }

            ToggleLever();
            StartCoroutine(MoveLift());
            return true;
        }
        return false;
    }

    public override bool StartInteraction()
    {
        return ActivateLift();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag(TagConstant.Player))
        {
            other.transform.SetParent(transform);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.CompareTag(TagConstant.Player))
        {
            other.GetComponent<Player>().ResetTransform();
        }
    }

    // 오브젝트가 파괴될 때 코루틴 정리
    void OnDestroy()
    {
        if (autoDescentCoroutine != null)
        {
            StopCoroutine(autoDescentCoroutine);
        }
    }
}