using UnityEngine;

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
    }

    public void RestartGame()
    {
        Player.Instance.RestartPlayer();

        HpUI.Instance.CreateAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }
}