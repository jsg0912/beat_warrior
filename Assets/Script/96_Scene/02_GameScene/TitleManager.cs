using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;
    public void StartGame()
    {
        SceneController.Instance.ChangeScene(SceneName.Tutorial2);
        InGameManager.TryCreateInGameManager();
        UIManager.Instance.SetInGameUIActive(true);
        GameManager.Instance.isInGame = true;
    }
    public void TitleSetting()
    {
        SettingUI.Instance.TurnOnPopup();
    }
}
