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
        bool success = Util.SetActive(menu, true);
        if (success) CommandManager.Instance?.popupSystemStack.Add(this);
    }

    public override void TurnOffPopup()
    {
        bool success = Util.SetActive(menu, false);
        if (success) CommandManager.Instance.PopPopupSystem();
    }

    public void OnClickContinue()
    {
        TurnOffPopup();
    }

    public void OnClickOption()
    {
        SettingUIManager.Instance.TurnOnSettingUI();
    }

    public void OnClickQuit()
    {
        SceneController.Instance.LoadTitle();
    }
}