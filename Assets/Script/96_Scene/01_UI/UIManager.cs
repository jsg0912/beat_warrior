using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void CheckCommand()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SceneReset();
        }
        if (Input.GetKeyDown(KeySetting.keys[PlayerAction.Interaction]))
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("asd");
            SetSettingActive();
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

    public void SetSettingActive()
    {
        isSettingActive = !isSettingActive;

        menuUI.SetSettingActive();
        menuUI.SetMenuActive();
        PauseControl.instance.SetPauseActive(isSettingActive);
    }
}