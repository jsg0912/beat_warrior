using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogName dialogName;

    public string GetDialog(DialogName dialogName, Language language, int dialogIndex)
    {
        if (dialogIndex >= DialogScript.DialogData[dialogName][language].Length)
        {
            return null;
        }

        return DialogScript.DialogData[dialogName][language][dialogIndex];
    }
}