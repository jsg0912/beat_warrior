using TMPro;
using UnityEngine;

public class MenuUI : PopupSystem
{
    public static MenuUI Instance;
    public GameObject menu;
    public TextMeshProUGUI[] txt;

    public override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public override void TurnOnPopup()
    {
        base.TurnOnPopup();
        Util.SetActive(menu, true);
        menu.transform.SetAsLastSibling();
    }

    public void OnClickClose()
    {
        TurnOffPopup();
    }

    public void OnClickSetting()
    {
        SettingUIManager.Instance.TurnOnSettingUI();
    }

    public void OnClickReStart()
    {
        GameManager.Instance.RestartGame();
    }
}