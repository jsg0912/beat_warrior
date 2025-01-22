using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    public int CurrentScene;
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

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        CurrentScene = currentScene.buildIndex;
    }

    public void ChangeSceneWithLoading(SceneName sceneName)
    {

    }

    public void ChangeScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Player:
                LoadingSceneManager.LoadScene("Player");
                //SceneManager.LoadScene("Player");
                CurrentScene = (int)SceneName.Player;
                break;
            case SceneName.Loading:
                SceneManager.LoadScene("Loading");
                break;
            case SceneName.ProtoType:
                LoadingSceneManager.LoadScene("ProtoType");
                //SceneManager.LoadScene("ProtoType");
                CurrentScene = (int)(SceneName.ProtoType);
                break;
            case SceneName.Title:
                LoadingSceneManager.LoadScene("Title");
                //SceneManager.LoadScene("Title");
                CurrentScene = (int)SceneName.Title;
                break;
            case SceneName.Setting:
                LoadingSceneManager.LoadScene("Setting");
                //SceneManager.LoadScene("Setting");
                CurrentScene = (int)SceneName.Setting;
                break;
            case SceneName.ProtoType2:
                LoadingSceneManager.LoadScene("ProtoType2");
                //SceneManager.LoadScene("ProtoType2");
                CurrentScene = (int)SceneName.ProtoType2;
                break;
            case SceneName.Tutorial1:
                LoadingSceneManager.LoadScene("Tutorial1");
                //SceneManager.LoadScene("Tutorial1");
                CurrentScene = (int)SceneName.Tutorial1;
                break;
            case SceneName.Tutorial2:
                LoadingSceneManager.LoadScene("Tutorial2");
                //SceneManager.LoadScene("Tutorial2");
                CurrentScene = (int)SceneName.Tutorial2;
                break;
            case SceneName.Village2:
                LoadingSceneManager.LoadScene("Village2");
                //SceneManager.LoadScene("Village2");
                CurrentScene = (int)SceneName.Village2;
                break;
        }
    }
}
