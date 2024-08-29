using UnityEngine;

public class SpiritObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.Instance.ChangeSpiritNumber(100); // TODO: Constant화 해야함
            Destroy(gameObject);
        }
    }
}
