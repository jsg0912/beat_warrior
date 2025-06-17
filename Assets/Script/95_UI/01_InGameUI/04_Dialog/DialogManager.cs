using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogName dialogName;

    public (DialogSpeaker, string[]) GetDialog(DialogName dialogName, Language language, int dialogIndex)
    {
        if (dialogIndex >= DialogScript.DialogData[dialogName][language].Length)
        {
            return (DialogSpeaker.Null, new string[] { });
        }

        return DialogScript.DialogData[dialogName][language][dialogIndex];
    }
}