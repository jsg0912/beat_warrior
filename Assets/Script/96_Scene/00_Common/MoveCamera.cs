using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform Target;
    public float speed;


    public Vector2 center;
    public Vector2 size;

    private float height;
    private float width;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        Target = Player.Instance.transform;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {
        cameraMovement();
        maxCameraMovement();

    }

    private void cameraMovement()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    private void maxCameraMovement()
    {
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

}

