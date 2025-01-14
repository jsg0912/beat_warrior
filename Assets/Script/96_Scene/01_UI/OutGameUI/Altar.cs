using UnityEngine;

public class Altar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConstant.Player))
        {
            UIManager.Instance.isTriggerAltar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            UIManager.Instance.isTriggerAltar = false;
        }
    }
}
