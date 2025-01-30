using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform Target;
    public float speed;
    public float z = -10f;
    public Vector2 center;
    public Vector2 size;

    private float orthographicSize;
    private float horizongraphicSize;

    public bool isCamera = true;

    private void Start()
    {
        orthographicSize = Camera.main.orthographicSize;
        horizongraphicSize = orthographicSize * Screen.width / Screen.height;
        Target = Player.Instance.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {
        Target = Player.Instance.transform;
        cameraMovement();
        maxCameraMovement();
    }

    private void cameraMovement()
    {
        if (isCamera)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        else
        {
            Vector3 targetPosition = new Vector3(Target.position.x, transform.position.y, z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    private void maxCameraMovement()
    {
        float lx = size.x * 0.5f - horizongraphicSize;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - orthographicSize;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, z);
    }
}