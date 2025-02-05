using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] float progress;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        SceneController.Instance.RunChangeSceneProcess(GameManager.Instance.currentScene);
        AsyncOperation loadingSceneProcess = SceneManager.LoadSceneAsync(GameManager.Instance.currentScene.ToString());
        loadingSceneProcess.allowSceneActivation = false;
        float timer = 0.0f;
        while (!loadingSceneProcess.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            if (loadingSceneProcess.progress < 0.9f)
            {
                progress = Mathf.Lerp(progress, loadingSceneProcess.progress, timer);
                if (progress >= loadingSceneProcess.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progress = Mathf.Lerp(progress, 1f, timer);
                if (progress == 1.0f)
                {
                    loadingSceneProcess.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}