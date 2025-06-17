using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public Transform Target;
    public float speed;
    public float z = -10f;
    public float yOffest = 2f;
    public Vector2 center;
    public Vector2 size;

    private float orthographicSize;
    private float horizontalGraphicSize;

    private bool isShakeing = false;

    public bool isCamera = true;

    private void Start()
    {
        orthographicSize = Camera.main.orthographicSize;
        horizontalGraphicSize = orthographicSize * Screen.width / Screen.height;

        if (Player.Instance != null)
        {
            Target = Player.Instance.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {
        if (Player.Instance == null) return;

        Target = Player.Instance.transform;

        if (!isShakeing)
        {
            cameraMovement();
            maxCameraMovement();
        }
    }

    private void cameraMovement()
    {
        if (Target == null) return;

        Vector3 targetPosition;

        if (isCamera)
        {
            targetPosition = new Vector3(Target.position.x, Target.position.y + yOffest, z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
        else
        {
            targetPosition = new Vector3(Target.position.x, transform.position.y, z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    private void maxCameraMovement()
    {
        float lx = size.x * 0.5f - horizontalGraphicSize;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - orthographicSize;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, z);
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        isShakeing = true;

        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
        isShakeing = false;

        Debug.Log(" 카메라 흔들림");
    }

    public static void Shake(float duration, float magnitude)
    {
        if (Camera.main != null)
        {
            var moveCam = Camera.main.GetComponent<MoveCamera>();
            if (moveCam != null)
            {
                moveCam.StartCoroutine(moveCam.CameraShake(duration, magnitude));
            }
        }
    }
}