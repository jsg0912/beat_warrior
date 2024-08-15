using UnityEngine;
using UnityEngine.UI;
public class ChangeSceneBtn : Button
{
    public void ChangeScene()
    {
        switch (this.gameObject.name)
        {
            case "PlayBtn":
                SceneController.instance.ChangeScene(SceneName.Player);
                break;
            case "SettingBtn":
                SceneController.instance.ChangeScene(SceneName.Setting);
                break;
            case "TittleBtn":
                SceneController.instance.ChangeScene(SceneName.Tittle);
                break;
            case "ExitBtn":
                Application.Quit();
                break;
        }
    }
}