using System.Collections;
using UnityEngine.SceneManagement;
public class SceneController : SingletonObject<SceneController>
{
    public void ChangeScene(SceneName sceneName)
    {
        RunChangeSceneProcess(sceneName);
        CheckBossStage(sceneName);
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
        StartCoroutine(ChangeSceneWithLoading(SceneName.Title));
    }

    public void CheckBossStage(SceneName scene)
    {
        switch(scene)
        {
            case SceneName.Ch2BossStage:
                SoundManager.Instance.BackGroundPlay(SoundList.Instance.chapter2BossBGM);
                SoundManager.Instance.PlayCh2BGSFX();
                UIManager.Instance.TurnOffMiniMap();
                break;
            default:
                UIManager.Instance.TurnOnMiniMap();
                break;
        }
    }
}
