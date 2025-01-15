using UnityEngine;

public class Altar : MonoBehaviour
{
    public InteractionPrompt interactionPrompt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConstant.Player))
        {
            UIManager.Instance.isTriggerAltar = true;
            interactionPrompt.ShowPrompt(PromptConstant.Sign);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            UIManager.Instance.isTriggerAltar = false;
            interactionPrompt.HidePrompt();
        }
    }
}
