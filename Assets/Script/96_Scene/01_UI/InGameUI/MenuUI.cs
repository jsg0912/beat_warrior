using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject Menu;
    private bool isMenuActive = false;
    private bool isSettingActive = false;
    [SerializeField] private GameObject Setting;
    public TextMeshProUGUI[] txt;

    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenuActive();
            PauseControl.instance.SetPauseActive();
        }
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void SetMenuActive()
    {
        isMenuActive = !isMenuActive;
        Menu.SetActive(isMenuActive);

        if (isMenuActive == false && isSettingActive == true) SetSettingActive();
    }

    public void SetSettingActive()
    {
        isSettingActive = !isSettingActive;
        Setting.SetActive(isSettingActive);
    }

    public void OnClickReStart()
    {
        GameManager.Instance.RestartGame();
    }

    public void ResumeButton()
    {
        SetMenuActive();
        PauseControl.instance.ResumeActive();
    }
}