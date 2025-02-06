using TMPro;
using UnityEngine;

// TODO: [Code Review - LJD] 현재 이 MenuUI는 InGameMenuManager(Monobehaviour)로 바뀌어야 하고, 실제 팝업은 InGameMenuUIPopup 등으로 분할되어야 함(AltarUI 관련 참고) - SDH< 20250201
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

    public override bool TurnOnPopup()
    {
        bool success = Util.SetActive(menu, true);
        if (success) PopupManager.Instance.PushPopup(this);
        return success;
    }

    public override bool TurnOffPopup()
    {
        bool success = Util.SetActive(menu, false);
        return success;
    }

    public void OnClickContinue()
    {
        TurnOffPopup();
        PopupManager.Instance.RemovePopup(this);
    }

    public void OnClickOption()
    {
        SettingUIManager.Instance.TurnOnSettingUI();
    }

    public void OnClickQuit()
    {
        TurnOffPopup();
        PopupManager.Instance.RemovePopup(this);
        SceneController.Instance.LoadTitle();
    }
}