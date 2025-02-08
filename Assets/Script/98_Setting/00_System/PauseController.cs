using UnityEngine;

public class PauseController : SingletonObject<PauseController>
{
    private bool isPauseActive;

    private void Start()
    {
        isPauseActive = false;
    }

    public void TryPauseGame()
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

    public void TryResumeGame()
    {
        if (isPauseActive)
        {
            Time.timeScale = 1.0f;
            isPauseActive = false;
        }
    }

    public bool IsPause() { return isPauseActive; }
}