public class GameModeSettingPopup : PopupSystem
{
    public void OnClickInfiniteMode()
    {
        GameManager.Instance.SetGameMode(GameMode.Infinite);
        GameManager.Instance.StartGame();
    }

    public void OnClickNormalMode()
    {
        GameManager.Instance.SetGameMode(GameMode.Normal);
        GameManager.Instance.StartGame();
    }
}
