using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private Language language = Language.kr;
    public Language Language => language;
    public bool IsLoading => SceneManager.GetActiveScene().name == SceneName.Loading.ToString();
    public SceneName currentScene;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public bool isInGame = false;

    public void Awake()
    {
        // ValidationChecker.Check();
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        SetDefaultCursor();
        isInGame = false;
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
        isInGame = true;
        InGameManager.TryCreateInGameManager();
        ChapterManager.Instance.StartNewGame();
    }

    public void RestartGame()
    {
        StartGame();
        Player.Instance.RestartPlayer();
    }
}