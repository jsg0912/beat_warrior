using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    public static SceneController instance;

    public SceneController(): base()
    {
        instance = this;
    }
    
    public void ChangeScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Player:
                SceneManager.LoadScene("Player");
                break;
            case SceneName.MainScene:
                SceneManager.LoadScene("MainScene");
                break;
            case SceneName.Setting:
                SceneManager.LoadScene("Setting");
                break;
        }
    }
}
