using UnityEngine;

public class SpiritObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.Instance.ChangeSpiritNumber(100); // TODO: Monster Drop Value
            Destroy(gameObject);
        }
    }
}
