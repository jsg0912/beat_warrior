using UnityEngine;

public class SoulObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.Instance.ChangeSoulNumber(100); // TODO: Constant화 해야함
            Destroy(gameObject);
        }
    }
}
