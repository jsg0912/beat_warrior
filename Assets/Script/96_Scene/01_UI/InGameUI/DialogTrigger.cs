using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogManager DialogManager;
    public GameObject DialogPanel;
    public bool isAction;
    public int dialogIndex;
    public TMPro.TextMeshProUGUI DialogText;

    public void OnClickStartDialog()
    {
        
        DialogManager = DialogManager.GetComponent<DialogManager>();
        StartDialog(DialogManager.DialogNumber);
        
        DialogPanel.SetActive(isAction);
    }
    public void StartDialog(int dialogNum)
    {
        string dialogDate = DialogManager.GetDialog(dialogNum, dialogIndex);

        if(dialogDate == null)
        {
            isAction = false;
            dialogIndex = 0;
            return;
        }
        DialogText.text = dialogDate;
        isAction = true;
        dialogIndex++;
    }
}
