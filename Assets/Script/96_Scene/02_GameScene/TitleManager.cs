using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void Start()
    {
        SoundManager.Instance.PlayTitleBGM();
    }

    public void OnPlayClick()
    {
        GameManager.Instance.StartGame();
    }

    public void OnSettingClick()
    {
        UIManager.Instance.SetActiveSettingPopup(true);
    }

    public void OnExitClick()
    {
        PopupManager.Instance.TurnOnGameExitPopup();
    }
}