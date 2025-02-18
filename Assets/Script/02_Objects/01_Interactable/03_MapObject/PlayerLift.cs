using UnityEngine;
using System.Collections;

public class PlayerLift : ObjectWithInteractionPrompt
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;

    private bool isMoving = false;
    private Vector3 targetPosition;

    private bool leverIsOn = false;

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
    }

    public bool ActivateLift()
    {
        if (!isMoving)
        {
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
}
