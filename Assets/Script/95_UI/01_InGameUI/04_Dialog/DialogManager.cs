using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    public GameObject dialogPanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogText;

    private DialogName currentDialogName;
    private (DialogSpeaker, string[])[] dialogSequence;
    private int dialogIndex = 0;
    private int lineIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogPanel.SetActive(false);
    }

    public void StartDialog(DialogName dialogName)
    {
        currentDialogName = dialogName;
        dialogSequence = DialogScript.DialogData[dialogName][GameManager.Instance.Language];

        dialogIndex = 0;
        lineIndex = 0;

        ShowCurrentLine();
        dialogPanel.SetActive(true);
        PauseController.Instance.TryPauseGame(); // 필요 시
    }

    private void Update()
    {
        if (!dialogPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0)) // 대사 진행
        {
            lineIndex++;

            var (_, lines) = dialogSequence[dialogIndex];
            if (lineIndex < lines.Length)
            {
                dialogText.text = lines[lineIndex];
            }
            else
            {
                dialogIndex++;
                if (dialogIndex < dialogSequence.Length)
                {
                    lineIndex = 0;
                    ShowCurrentLine();
                }
                else
                {
                    EndDialog();
                }
            }
        }
    }

    private void ShowCurrentLine()
    {
        var (speaker, lines) = dialogSequence[dialogIndex];
        speakerText.text = speaker.ToString(); // 한글 이름 매핑 필요 시 Dictionary 사용 가능
        dialogText.text = lines[lineIndex];
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        PauseController.Instance.TryResumeGame(); // 필요 시
    }
}