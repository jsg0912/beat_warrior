using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void Start() { Initialize(); }
    public void OnEnable() { Initialize(); }

    public void OnPlayClick()
    {
        PopupManager.Instance.TurnOnGameModeSettingPopup();
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
        if (Player.Instance != null) Destroy(Player.Instance.gameObject);
    }
}