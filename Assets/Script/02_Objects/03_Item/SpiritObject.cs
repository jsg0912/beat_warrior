using UnityEngine;

public class SpiritObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.Instance.IncreaseSpirit(1);
            Destroy(gameObject);
        }
    }
}
