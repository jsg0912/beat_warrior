using UnityEngine;

public class GameExitPopup : PopupSystem
{
    public override BlurType SetBlur()
    {
        return BlurType.BlackBlur;
    }

    override public void OnClickOkay()
    {
        Application.Quit();
    }
}
