using UnityEngine.SceneManagement;

public class SceneController : SingletonObject<SceneController>
{
    public void ChangeScene(SceneName sceneName)
    {
        RunChangeSceneProcess(sceneName);
        SceneManager.LoadScene(sceneName.ToString());
    }

    public void ChangeSceneWithLoading(SceneName targetScene)
    {
        switch (targetScene)
        {
            case SceneName.Loading:
                break;
            default:
                GameManager.Instance.currentScene = targetScene;
                ChangeScene(SceneName.Loading);
                break;
        }
    }

    public void RunChangeSceneProcess(SceneName sceneName)
    {
        if (sceneName != SceneName.Loading)
        {
            UIManager.Instance.SetInGameUIActive();
        }
    }

    public void LoadTitle()
    {
        SoundManager.Instance.PlayTitleBGM();
        ChangeSceneWithLoading(SceneName.Title);
    }
}
