using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Language language = Language.kr;
    public static UIManager Instance;
    public bool isTriggerAltar = false;
    public AltarPopup altarPopup;
    public MenuUI menuUI;
    public GameObject inGameUIPrefab;
    public GameObject settingPrefab;
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


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SceneReset();
        }
        if (Input.GetKeyDown(KeySetting.keys[Action.Interaction]))
        {
            UIInteraction();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (altarPopup.isOn == true && menuUI.GetMenuActive() == false)
            {
                altarPopup.HideAltarPopup();
            }
            else if (altarPopup.isOn == false)
            {
                menuUI.SetMenuActive();
                PauseControl.instance.SetPauseActive();
            }
        }
    }

    private void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UIInteraction()
    {
        if (isTriggerAltar == true && menuUI.GetMenuActive() == false)
        {
            altarPopup.ShowAltarPopup();
        }
    }

    public void SetInGameUIActive(bool active)
    {
        Util.SetActive(inGameUIPrefab, active);
    }

    /*public void SetSettingActive()
    {
        isSettingActive = !isSettingActive;
        // [Code Review - LJD] ?????? unnecessary if-else statement - SDH, 20250114 
        if (isSettingActive)
        {
            Util.SetActive(menuPrefab, isSettingActive);
            Util.SetActive(settingPrefab, isSettingActive);
        }
        else
        {
            Util.SetActive(settingPrefab, isSettingActive);
            Util.SetActive(menuPrefab, isSettingActive);
        }
    }*/
}

