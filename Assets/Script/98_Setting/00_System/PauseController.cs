using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController instance;
    private bool isPauseActive;
    private float defaultGameSpeed = 1.0f;

    private void Awake()
    {
        instance = this;
    }

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
        defaultGameSpeed = speed;
        Time.timeScale = speed;
    }

    public void TryResumeGame()
    {
        if (isPauseActive)
        {
            Time.timeScale = defaultGameSpeed;
            isPauseActive = false;
        }
    }

    public bool IsPause() { return isPauseActive; }
}