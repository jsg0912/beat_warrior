using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Language language = Language.kr;
    public bool isTriggerAltar = false;
    public AltarPopup altarPopup;
    public MenuUI menuUI;
    public GameObject inGameUIPrefab;
    private bool isSettingActive = false;

    // [Code Review - LJD] Make PopupSystem Queue for "ESC" Process - SDH, 20250114

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static void CreateUI()
    {
        GameObject UI = Instantiate(Resources.Load<GameObject>(PrefabRouter.UIPrefab));
        DontDestroyOnLoad(UI);
    }

    public void TurnOnAltarPopup()
    {
        altarPopup.TurnOnPopup();
    }

    public void SetInGameUIActive(bool active)
    {
        Util.SetActive(inGameUIPrefab, active);
    }
}