using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class StartSceneInitializer
{
    private static SceneName StartSceneName = SceneName.Title; // 시작할 씬 경로

    static StartSceneInitializer()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            if (SceneManager.GetActiveScene().name != StartSceneName.ToString())
            {
                SceneManager.LoadScene(SceneName.Title.ToString());
            }
        }
    }
}
