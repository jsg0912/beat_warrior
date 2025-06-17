using UnityEngine;

public class DialogTrigger : ObjectWithInteractionPrompt
{
    public DialogName dialogName;

    public override bool StartInteraction()
    {
        DialogManager.Instance.StartDialog(dialogName);
        return true;    
    }
}