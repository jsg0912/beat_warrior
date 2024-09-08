using UnityEngine;

public class PauseControl : MonoBehaviour
{
    private bool isPauseActive;

    public static PauseControl instance;

    private void Start()
    {
        instance = this;
        isPauseActive = false;
    }


    public void PauseActive()
    {

        if (!isPauseActive)
        {
            Time.timeScale = 0;
            isPauseActive = true;
        }
    }

    public void ResumeActive()
    {
        if (isPauseActive)
        {
            Time.timeScale = 1.0f;
            isPauseActive = false;
        }
    }

    public void SetPauseActive()
    {

        if (isPauseActive == false) { PauseActive(); }
        else { ResumeActive(); }

    }




}
