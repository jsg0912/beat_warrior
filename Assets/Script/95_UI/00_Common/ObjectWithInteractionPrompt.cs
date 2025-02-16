using UnityEngine;
using TMPro;

public abstract class ObjectWithInteractionPrompt : MonoBehaviour
{
    public TextMeshPro promptText;
    protected bool isInitialized = false;

    protected void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;
    }

    public abstract bool StartInteraction();

    public virtual void SetInteractAble()
    {
        Initialize();
        bool success = SetActivePromptText(true);
        if (success) InteractionManager.Instance.AddObject(this);
        promptText.text = PromptMessageGenerator.GeneratePromptMessage(PlayerAction.Interaction);
    }

    public virtual void SetInteractDisable()
    {
        bool success = SetActivePromptText(false);
        if (success) InteractionManager.Instance.RemoveObject(this);
    }

    protected bool SetActivePromptText(bool isActive) { return Util.SetActive(promptText.gameObject, isActive); }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            SetInteractAble();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstant.Player))
        {
            SetInteractDisable();
        }
    }
}