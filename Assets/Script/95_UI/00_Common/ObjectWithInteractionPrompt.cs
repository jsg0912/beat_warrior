using UnityEngine;
using TMPro;

public abstract class ObjectWithInteractionPrompt : MonoBehaviour
{
    public TextMeshPro promptText;
    private bool isInitialized = false;

    private void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;
    }

    public void ShowPrompt()
    {
        Initialize();
        Util.SetActive(promptText.gameObject, true);

        promptText.text = PromptMessageGenerator.GeneratePromptMessage();
    }

    public void HidePrompt() { Util.SetActive(promptText.gameObject, false); }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            ShowPrompt();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            HidePrompt();
        }
    }
}