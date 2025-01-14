using UnityEngine;

public class Marker : MonoBehaviour
{
    private const float markerSpeed = 15.0f;

    private void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }

    private void FixedUpdate()
    {
        //transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * 500);
    }

    public void SetVelocity(Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * markerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag(TagConstant.Monster) && obj.GetComponent<Monster>().GetIsAlive())
        {
            Player.Instance.SetTarget(obj);
            obj.GetComponent<Monster>().SetTarget();
            Destroy(this.gameObject);
        }
    }

    public void DestroyMarker()
    {
        Destroy(this.gameObject);
    }
}
