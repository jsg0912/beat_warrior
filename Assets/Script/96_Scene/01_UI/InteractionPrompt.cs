using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public TextMeshPro promptText;
    private Dictionary<string, string> interactionMessages;
    private bool isInitialized = false;

    private void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        KeyCode interactionKey = KeySetting.keys[Action.Interaction];
        interactionMessages = new Dictionary<string, string>()
        {
            {"Sign", $"Press [{interactionKey}]" }
        };
    }
    public void ShowPrompt(string interactionType)
    {
        Initialize();
        Util.SetActive(gameObject, true);
        if (interactionMessages.ContainsKey(interactionType))
        {
            promptText.text = interactionMessages[interactionType];
        }
    }

    public void HidePrompt() { Util.SetActive(gameObject, false); }

    public void UpdateInteractionMassages()
    {
        
    }
}
