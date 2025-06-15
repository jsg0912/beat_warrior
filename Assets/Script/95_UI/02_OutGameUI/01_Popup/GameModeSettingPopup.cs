using TMPro;

public class GameModeSettingPopup : PopupSystem
{
    public TMP_Text infiniteModeText;
    public TMP_Text normalModeText;

    public void OnEnable()
    {
        infiniteModeText.text = ScriptPool.GameModeText[GameMode.Infinite][GameManager.Instance.Language];
        normalModeText.text = ScriptPool.GameModeText[GameMode.Normal][GameManager.Instance.Language];
    }

    public void OnClickInfiniteMode()
    {
        GameManager.Instance.SetGameMode(GameMode.Infinite);
        GameManager.Instance.StartGame();
        TurnOffPopup();
    }

    public void OnClickNormalMode()
    {
        GameManager.Instance.SetGameMode(GameMode.Normal);
        GameManager.Instance.StartGame();
        TurnOffPopup();
    }
}
