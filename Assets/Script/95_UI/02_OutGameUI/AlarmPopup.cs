using TMPro;

public class AlarmPopup : PopupSystem
{
    public static AlarmPopup instance;
    public override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public TextMeshProUGUI txtContent;

    public void SetContent(string content)
    {
        txtContent.text = content;
    }
}