using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    public DialogManager DialogManager;
    public GameObject dialogPanelPrefab; 

    private GameObject dialogPanel;
    private TextMeshProUGUI dialogText;
    public bool isPlayerInTrigger = false; 
    private int dialogIndex = 0; 

    private void Start()
    {
        dialogPanel = Instantiate(dialogPanelPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        dialogPanel.SetActive(false);
        dialogText = dialogPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeySetting.keys[Action.Interaction]))
        {
            StartDialog();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            dialogIndex = 0;
            dialogPanel.SetActive(false);
        }
    }

    public void StartDialog()
    {
        string dialogData = DialogManager.GetDialog(DialogManager.dialogName, UIManager.Instance.language, dialogIndex);

        if (dialogData == null)
        {
            dialogIndex = 0; 
            dialogPanel.SetActive(false);
            PauseControl.instance.ResumeActive();
            return;
        }

        dialogPanel.SetActive(true);
        dialogText.text = dialogData;
        dialogIndex++;
        PauseControl.instance.PauseActive();
    }
}