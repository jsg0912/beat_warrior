using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal Instance;
    public bool IsTriggerPortal;

    private void Start()
    {
        IsTriggerPortal = false;
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagConstant.Player)) IsTriggerPortal = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagConstant.Player)) IsTriggerPortal = false;
    }

}
