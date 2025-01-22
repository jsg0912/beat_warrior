using UnityEngine;
using UnityEngine.UI;
public class ChangeSceneBtn : Button
{
    public void ChangeScene()
    {
        switch (this.gameObject.name)
        {
            case "PlayBtn":
                SceneController.Instance.ChangeScene(SceneName.Player);
                break;
            case "SettingBtn":
                SceneController.Instance.ChangeScene(SceneName.Setting);
                break;
            case "TittleBtn":
                SceneController.Instance.ChangeScene(SceneName.Title);
                break;
            case "ExitBtn":
                Application.Quit();
                break;
        }
    }
}