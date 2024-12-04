using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (CompareTag(TagConstant.Player))
        {
            Player.Instance.GetDamaged(1);
            Destroy(this.gameObject);
        }
    }

    private void DestroyArrow()
    {
        Destroy(this.gameObject);
    }
}
