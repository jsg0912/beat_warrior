using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogName dialogName;

    public string GetDialog(DialogName dialogName, Language language, int DialogIndex)
    {
        if (DialogIndex == DialogScript.DialogData[dialogName][language].Length)
        {
            return null;
        }
        else
        {
            return DialogScript.DialogData[dialogName][language][DialogIndex];
        }
    }

}