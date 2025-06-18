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

    // TODO: 이렇게 따로 하지말고, PopupSystem에서 Esc로 무시할 수 있는 애인지 아닌지를 설정할 수 있고, 그것에 따라 Common에서 처리하도록 변경
    // 이렇게 하면, esc로 안사라진다.
    public override bool TurnOnPopup()
    {
        bool success = Util.SetActive(gameObject, true);
        return success;
    }
}