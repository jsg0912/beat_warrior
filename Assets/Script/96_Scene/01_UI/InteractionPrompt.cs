using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public TextMeshPro promptText;
    private Dictionary<string, string> interactionMessages;
    private bool isInitialized = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) { UpdateInteractionMessages(); }
    }
    private void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        KeyCode interactionKey = KeySetting.keys[PlayerAction.Interaction];
        interactionMessages = new Dictionary<string, string>()
        {
            {"Sign", $"Press [{interactionKey}]" },
            {"Altar", $"Press [{interactionKey}]"}
        };
    }
    public void ShowPrompt(string interactionType)
    {
        Initialize();
        UpdateInteractionMessages();
        Util.SetActive(gameObject, true);

        if (interactionMessages.ContainsKey(interactionType))
        {
            promptText.text = interactionMessages[interactionType];
        }
    }

    public void HidePrompt() { Util.SetActive(gameObject, false); }

    public void UpdateInteractionMessages()
    {
        KeyCode interactionKey = KeySetting.keys[PlayerAction.Interaction];

        interactionMessages["Sign"] = $"Press [{interactionKey}]";
        interactionMessages["Altar"] = $"Press[{interactionKey}]";
    }
}
