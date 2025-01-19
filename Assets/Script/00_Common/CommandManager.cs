using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        if (!PauseControl.instance.GetPause())
        {
            Player.Instance.CheckIsMove();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseControl.instance.SetPauseActive();
        }

        UIManager.Instance.CheckCommand();

        // When we paused the game, we don't want to check below commands.
        if (!PauseControl.instance.GetPause())
        {
            Player.Instance.CheckPlayerCommand();
        }
    }
}