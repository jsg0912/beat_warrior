using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogManager DialogManager;
    public GameObject DialogPanel;
    public bool isAction;
    public int dialogIndex;
    //public TMPro.TextMeshPro DialogText;
    public TMPro.TextMeshProUGUI DialogText;

    public void OnClickStartDialog()
    {
        if(isAction) isAction = false;
        else
        {
            isAction = true;
            DialogManager = DialogManager.GetComponent<DialogManager>();
            StartDialog(DialogManager.DialogNumber);
        }
        DialogPanel.SetActive(isAction);
    }
    public void StartDialog(int dialogNum)
    {
        string dialogDate = DialogManager.GetDialog(dialogNum, dialogIndex);

        DialogText.text = dialogDate;
    }
}
