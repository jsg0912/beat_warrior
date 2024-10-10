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
        StartDialog(DialogManager.dialogName);

        DialogPanel.SetActive(isAction);
    }
    public void StartDialog(DialogName dialogName)
    {
        string dialogDate = DialogManager.GetDialog(dialogName, UIManager.Instance.language, dialogIndex);

        if (dialogDate == null)
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
