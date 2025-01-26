using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private Language language = Language.kr;
    public Language Language => language;
    public Texture2D cursorIcon;
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
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        SetMouseCursor();
        isInGame = false;
        Enum.TryParse(SceneManager.GetActiveScene().name, true, out currentScene);
    }

    public void SetLanguage(Language language)
    {
        this.language = language;
    }

    private void SetMouseCursor()
    {
        if (cursorIcon != null) Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
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