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
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        // TODO: Player Scene은 임시로 이동함 - 신동환, 2024.09.11
        SceneController.Instance.ChangeScene(SceneName.ProtoType);
    }

    public void RestartGame()
    {
        StartGame();

        Player.Instance.RestartPlayer();

        HpUI.Instance.SetAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }
}