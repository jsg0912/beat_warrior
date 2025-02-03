using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;
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
        SettingUIManager.Instance.TurnOnSettingUI();
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}