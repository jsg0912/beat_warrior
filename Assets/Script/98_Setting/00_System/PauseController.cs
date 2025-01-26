using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController instance;
    private bool isPauseActive;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isPauseActive = false;
    }

    public void PauseGame()
    {

        if (!isPauseActive)
        {
            Time.timeScale = 0;
            isPauseActive = true;
        }
    }

    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    public void ResumeGame()
    {
        if (isPauseActive)
        {
            Time.timeScale = 1.0f;
            isPauseActive = false;
        }
    }

    public bool GetPause() { return isPauseActive; }
}