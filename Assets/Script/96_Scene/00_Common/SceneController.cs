using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneController>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SceneController");
                    _instance = go.AddComponent<SceneController>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

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
