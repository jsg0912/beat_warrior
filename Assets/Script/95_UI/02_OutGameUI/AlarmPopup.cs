// [Code Review - LJD]: ObjectPool 사용하도록 수정, Singleton 쓸게 아님 - SDH, 20250208
using TMPro;

public class AlarmPopup : PopupSystem
{
    public TextMeshProUGUI txtContent;

    public void SetContent(string content)
    {
        txtContent.text = content;
    }
}