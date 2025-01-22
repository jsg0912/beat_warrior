using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] Image progressBar;

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
            timer += Time.deltaTime;
            if (loadingSceneProcess.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, loadingSceneProcess.progress, timer);
                if (progressBar.fillAmount >= loadingSceneProcess.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    loadingSceneProcess.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}