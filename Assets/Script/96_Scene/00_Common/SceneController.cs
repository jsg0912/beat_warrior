using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : SingletonObject<SceneController>
{
    public SceneName currentScene { get; private set; }

    void Start()
    {
        currentScene = Util.ParseEnumFromString<SceneName>(SceneManager.GetActiveScene().name);
    }

    public void ChangeScene(SceneName sceneName)
    {
        RunChangeSceneProcess(sceneName);
        SceneManager.LoadScene(sceneName.ToString());
    }

    public IEnumerator ChangeSceneWithLoading(SceneName targetScene)
    {
        yield return StartCoroutine(FadeManager.Instance.FadeOut());
        switch (targetScene)
        {
            case SceneName.Loading:
                break;
            default:
                currentScene = targetScene;
                ChangeScene(SceneName.Loading);
                break;
        }
        yield return null;
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
        StartCoroutine(ChangeSceneWithLoading(SceneName.Title));
    }

    public bool GetIsBossStage(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Ch2BossStage:
                return true;
            default:
                return false;
        }
    }
}
