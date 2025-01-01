using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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

    public void Awake()
    {
        TraitPriceList.CheckTraitPriceListValidation();
        UIManager.CreateUI();
        Player.TryCreatePlayer();
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        // TODO: Title Scene으로 시작하도록 변경함 - 신동환, 2024.11.21
        SceneController.Instance.ChangeScene(SceneName.Tittle);
    }

    public void RestartGame()
    {
        StartGame();

        Player.Instance.RestartPlayer();

        PlayerHpUI.Instance.HpInitialize();
    }
}