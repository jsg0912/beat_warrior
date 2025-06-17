using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    public GameObject dialogPanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogText;
    public Image speakerImage;

    public List<SpeakerSprite> speakerSprites; // �ν����� ����� ����Ʈ
    private Dictionary<DialogSpeaker, Sprite> speakerSpriteDict;

    private DialogName currentDialogName;
    private (DialogSpeaker, string[])[] dialogSequence;
    private int dialogIndex = 0;
    private int lineIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogPanel.SetActive(false);
        speakerSpriteDict = new Dictionary<DialogSpeaker, Sprite>();

        foreach (var entry in speakerSprites)
        {
            speakerSpriteDict[entry.speaker] = entry.Sprite;
        }
    }

    public void StartDialog(DialogName dialogName)
    {
        currentDialogName = dialogName;
        dialogSequence = DialogScript.DialogData[dialogName][GameManager.Instance.Language];

        dialogIndex = 0;
        lineIndex = 0;

        ShowCurrentLine();
        dialogPanel.SetActive(true);
        PauseController.Instance.TryPauseGame();
    }

    private void Update()
    {
        if (!dialogPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
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
        speakerText.text = speaker.ToString();
        dialogText.text = lines[lineIndex];

        if (speakerSpriteDict.TryGetValue(speaker, out var sprite))
        {
            speakerImage.sprite = sprite;
            speakerImage.enabled = true;
        }
        else
        {
            speakerImage.enabled = false;
        }
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        PauseController.Instance.TryResumeGame();
    }
}