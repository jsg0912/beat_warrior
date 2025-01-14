using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Language language = Language.kr;
    public static UIManager Instance;
    public bool isTriggerAltar = false;
    public GameObject altarPrefab;
    private AlterPopup alterPopup;
    public GameObject menuPrefab;
    private MenuUI menuUI;
    public GameObject inGameUIPrefab;


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

    private void Start()
    {
        alterPopup = altarPrefab.GetComponent<AlterPopup>();
        menuUI = menuPrefab.GetComponent<MenuUI>();
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
            if(alterPopup.isOn == true && menuUI.GetMenuActive() == false)
            {
                alterPopup.HideAltarPopup();
            }
            else if(alterPopup.isOn == false)
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
            alterPopup.ShowAltarPopup();
        }
    }

    public void SetInGameUIActive(bool active)
    {
        inGameUIPrefab.SetActive(active);
    }
}

