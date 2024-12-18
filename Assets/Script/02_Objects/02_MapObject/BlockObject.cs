using UnityEngine;

public class BlockObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Mark))
        {
            Marker.Destroy(collision.gameObject);
        }
        else if (collision.CompareTag(TagConstant.Projectile))
        {
            Arrow.Destroy(collision.gameObject);
        }
    }
}
