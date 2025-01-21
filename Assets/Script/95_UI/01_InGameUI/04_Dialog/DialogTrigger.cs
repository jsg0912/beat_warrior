using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    public DialogManager DialogManager;
    public GameObject dialogPanelPrefab;
    public DialogName dialogName;

    private GameObject dialogPanel;
    private TextMeshProUGUI dialogText;
    public bool isPlayerInTrigger = false;
    private int dialogIndex = 0;

    private void Start()
    {
        dialogPanel = Instantiate(dialogPanelPrefab, transform.position + Vector3.up * 2, Quaternion.identity);

        Util.SetActive(dialogPanel, false);
        dialogText = dialogPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeySetting.keys[PlayerAction.Interaction]))
        {
            StartDialog();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConstant.Player))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(TagConstant.Player))
        {
            isPlayerInTrigger = false;
            dialogIndex = 0;
            Util.SetActive(dialogPanel, false);
        }
    }

    public void StartDialog()
    {
        string dialogData = DialogManager.GetDialog(DialogManager.dialogName, UIManager.Instance.language, dialogIndex);

        if (dialogData == null)
        {
            dialogIndex = 0;
            Util.SetActive(dialogPanel, false);
            PauseController.instance.ResumeGame();
            return;
        }

        Util.SetActive(dialogPanel, true);
        dialogText.text = dialogData;
        dialogIndex++;
        PauseController.instance.PauseGame();
    }
}