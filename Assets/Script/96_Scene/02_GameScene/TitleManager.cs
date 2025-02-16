using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void Start() { Initialize(); }
    public void OnEnable() { Initialize(); }

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

    private void Initialize()
    {
        SoundManager.Instance.PlayTitleBGM();
        GameManager.Instance.SetDefaultCursor();
        Destroy(Player.Instance);
    }
}