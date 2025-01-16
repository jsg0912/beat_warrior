using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public InteractionPrompt interactionPrompt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            
            interactionPrompt.ShowPrompt(PromptConstant.Sign);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            
            interactionPrompt.HidePrompt();
        }
    }
}
