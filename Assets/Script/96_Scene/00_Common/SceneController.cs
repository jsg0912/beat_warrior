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

    public void ChangeScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Player:
                SceneManager.LoadScene("Player");
                CurrentScene = (int)SceneName.Player;
                break;
            case SceneName.ProtoType:
                SceneManager.LoadScene("ProtoType");
                CurrentScene = (int)(SceneName.ProtoType);
                break;
            case SceneName.Tittle:
                SceneManager.LoadScene("Tittle");
                CurrentScene = (int)SceneName.Tittle;
                break;
            case SceneName.Setting:
                SceneManager.LoadScene("Setting");
                CurrentScene = (int)SceneName.Setting;
                break;
            case SceneName.ProtoType2:
                SceneManager.LoadScene("ProtoType2");
                CurrentScene = (int)SceneName.ProtoType2;
                break;
            case SceneName.Village1:
                SceneManager.LoadScene("Village1");
                CurrentScene = (int)SceneName.ProtoType2;
                break;
            case SceneName.Village2:
                SceneManager.LoadScene("Village2");
                CurrentScene = (int)SceneName.ProtoType2;
                break;
        }
    }
}
