using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Texture2D cursorIcon;

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
        ValidationChecker.Check();
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        SetMouseCursor();
        UIManager.CreateUI();
        isInGame = true;
    }

    private void SetMouseCursor()
    {
        if (cursorIcon != null) Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
    }

    public void StartGame()
    {
        SceneController.Instance.ChangeScene(SceneName.Tutorial2);
        InGameManager.TryCreateInGameManager();
        UIManager.Instance.SetInGameUIActive(true);
    }

    public void RestartGame()
    {
        StartGame();
        Player.Instance.RestartPlayer();
    }

    // public void TitleSetting()
    // {
    //     UIManager.Instance.SetSettingActive();
    // }
}