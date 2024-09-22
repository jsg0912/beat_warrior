using UnityEngine;

public class BlockObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: 오브젝트 이름으로 찾는 것 금지, Tag로 일괄처리하도록 수정 - 신동환, 2024.09.13
        if (collision.tag == "Mark")
        {
            Marker.Destroy(collision.gameObject);
        }
        else if (collision.tag == "Projectile")
        {
            Arrow.Destroy(collision.gameObject);
        }
    }
}
