using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // [Code Review - LJD] Variables Naming Irregular: UpperCase&LowerCase - SDH, 20250114
    public GameObject Menu;
    private bool isMenuActive = false;
    private bool isSettingActive = false;
    [SerializeField] private GameObject Setting;
    public TextMeshProUGUI[] txt;
    public AltarPopup AltarPopup;

    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && AltarPopup.IsAltarPopupOn() == false)
        {
            SetMenuActive();
            PauseControl.instance.SetPauseActive();
        }
        // [Code Review - LJD] Do not use update for one-time event - SDH, 20250114
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    public void SetMenuActive()
    {
        isMenuActive = !isMenuActive;
        Util.SetActive(Menu, isMenuActive);

        // [Code Review - LJD] cannot understand why this condition is correct - SDH, 20250114
        if (isMenuActive == false && isSettingActive == true) SetSettingActive();
    }

    public void SetSettingActive()
    {
        isSettingActive = !isSettingActive;
        Util.SetActive(Setting, isSettingActive);
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
}