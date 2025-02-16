using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class StartSceneInitializer
{
    private static string StartScenePath = "Assets/Scenes/" + SceneName.Title; // 시작할 씬 경로

    static StartSceneInitializer()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            if (!EditorSceneManager.GetActiveScene().path.Equals(StartScenePath))
            {
                Debug.Log($"[StartSceneInitializer] 게임을 항상 {StartScenePath} 씬에서 시작합니다.");

                SceneManager.LoadScene(SceneName.Title.ToString());
            }
        }
    }
}
