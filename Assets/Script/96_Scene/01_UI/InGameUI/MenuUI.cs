using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menu;
    private bool isMenuActive = false;
    private bool isSettingActive = false;
    [SerializeField] private GameObject setting;
    public TextMeshProUGUI[] txt;
    public AltarPopup altarPopup;

    private void Start()
    {
        UpdateKeySettingUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && altarPopup.IsAltarPopupOn() == false)
        {
            SetMenuActive();
            UpdateKeySettingUI();
            PauseControl.instance.SetPauseActive();
        }
    }

    public void SetMenuActive()
    {
        isMenuActive = !isMenuActive;
        Util.SetActive(menu, isMenuActive);

        if (isMenuActive == false && isSettingActive == true) SetSettingActive();
    }

    public void SetSettingActive()
    {
        isSettingActive = !isSettingActive;
        Util.SetActive(setting, isSettingActive);
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

    public bool GetMenuActive()
    {
        return isMenuActive;
    }

    private void UpdateKeySettingUI()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }
}