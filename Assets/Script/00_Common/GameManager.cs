using System;
using UnityEngine.SceneManagement;

public class GameManager : SingletonObject<GameManager>
{
    private Language language = Language.kr;
    public Language Language => language;
    public bool IsLoading => SceneManager.GetActiveScene().name == SceneName.Loading.ToString();

    public bool isInGame { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        // ValidationChecker.Check();
    }

    public void Start()
    {
        SetIsInGame(false);
    }

    public void SetLanguage(Language language)
    {
        this.language = language;
    }

    public void SetDefaultCursor()
    {
        if (isInGame)
        {
            CursorController.Instance.SetInGameCursor();
        }
        else
        {
            CursorController.Instance.SetTitleCursor();
        }
    }

    public void StartGame()
    {
        SetIsInGame(true);
        PauseController.Instance.ResetSpeed();
        InGameManager.TryCreateSingletonObject();
        ChapterManager.Instance.StartNewGame();
        Player.TryCreatePlayer();
    }

    public void RestartGame()
    {
        StartGame();
        Player.Instance.RestartPlayer();
    }

    public void RestartCurrentStage()
    {
        ChapterManager.Instance.RestartCurrentStage();
    }


    public void QuitInGame()
    {
        SetIsInGame(false);
        SceneController.Instance.LoadTitle();
        // TODO: Save Game
    }

    public void SetIsInGame(bool isInGame)
    {
        this.isInGame = isInGame;
        SetDefaultCursor();
    }
}