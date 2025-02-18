public class GameOverPopup : PopupSystem
{
    public override BlurType SetBlur()
    {
        return BlurType.None;
    }

    override public void OnClickOkay()
    {
        base.OnClickOkay();
        GameManager.Instance.RestartCurrentStage();
    }

    override public void OnClickCancel()
    {
        base.OnClickCancel();
        GameManager.Instance.QuitInGame();
    }

    // 이렇게 하면, esc로 안사라진다.
    public override bool TurnOnPopup()
    {
        bool success = Util.SetActive(gameObject, true);
        return success;
    }
}