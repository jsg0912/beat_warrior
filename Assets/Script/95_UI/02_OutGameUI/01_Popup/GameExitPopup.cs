using UnityEngine;

public class GameExitPopup : PopupSystem
{
    public override BlurType SetBlur()
    {
        return BlurType.TopPopupBlackBlur;
    }

    override public void OnClickOkay()
    {
        Application.Quit();
    }
}
