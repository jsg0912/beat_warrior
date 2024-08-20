using UnityEngine;

public class SpiritObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.inventory.IncreaseSpirit(1);
            Destroy(gameObject);
        }
    }
}
