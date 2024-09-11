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
        switch (sceneName)
        {
            case SceneName.Player:
                SceneManager.LoadScene("Player");
                break;
            case SceneName.ProtoType:
                SceneManager.LoadScene("ProtoType");
                break;
            case SceneName.Tittle:
                SceneManager.LoadScene("Tittle");
                break;
            case SceneName.Setting:
                SceneManager.LoadScene("Setting");
                break;
        }
    }
}
