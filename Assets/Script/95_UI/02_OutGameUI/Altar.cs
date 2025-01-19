using UnityEngine;

public class Altar : ObjectWithInteractionPrompt
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag(TagConstant.Player))
        {
            UIManager.Instance.isTriggerAltar = true;
        }

    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag(TagConstant.Player))
        {
            UIManager.Instance.isTriggerAltar = false;
        }
    }
}