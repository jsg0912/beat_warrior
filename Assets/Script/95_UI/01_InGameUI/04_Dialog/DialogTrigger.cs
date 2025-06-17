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
        if (isPlayerInTrigger && Input.GetKeyDown(KeySetting.GetKey(PlayerAction.Interaction)))
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

    // TODO: Dialog 진행과정 수정 필요
    public void StartDialog()
    {
        var dialogData = DialogManager.GetDialog(DialogManager.dialogName, GameManager.Instance.Language, dialogIndex);

        if (dialogData.Item1 == DialogSpeaker.Null)
        {
            dialogIndex = 0;
            Util.SetActive(dialogPanel, false);
            PauseController.Instance.TryResumeGame();
            return;
        }

        Util.SetActive(dialogPanel, true);
        dialogText.text = "";
        dialogIndex++;
        PauseController.Instance.TryPauseGame();
    }
}