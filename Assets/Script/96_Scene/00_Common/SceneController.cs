using System.Collections;
using UnityEngine.SceneManagement;
public class SceneController : SingletonObject<SceneController>
{
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
                GameManager.Instance.currentScene = targetScene;
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
        SoundManager.Instance.PlayTitleBGM();
        StartCoroutine( ChangeSceneWithLoading(SceneName.Title));
    }
}
