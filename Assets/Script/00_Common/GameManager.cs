using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonObject<GameManager>
{
    private Language language = Language.kr;
    public Language Language => language;
    public bool IsLoading => SceneManager.GetActiveScene().name == SceneName.Loading.ToString();
    public SceneName currentScene;
    public bool isInGame { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        // ValidationChecker.Check();
    }

    public void Start()
    {
        SetDefaultCursor();
        SetIsInGame(false);
        SetDefaultCursor();
        Enum.TryParse(SceneManager.GetActiveScene().name, true, out currentScene);
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
        InGameManager.TryCreateSingletonObject();
        ChapterManager.Instance.StartNewGame();
        Player.TryCreatePlayer();
    }

    public void RestartGame()
    {
        StartGame();
        Player.Instance.RestartPlayer();
    }

    public void QuitInGame()
    {
        SetIsInGame(false);
        Destroy(InGameManager.Instance.gameObject);
        // TODO: Save Game
    }

    public void SetIsInGame(bool isInGame)
    {
        this.isInGame = isInGame;
    }
}