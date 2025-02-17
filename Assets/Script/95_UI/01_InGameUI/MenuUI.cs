using UnityEngine;

// TODO: [Code Review - LJD] 현재 이 MenuUI는 InGameMenuManager(Monobehaviour)로 바뀌어야 하고, 실제 팝업은 InGameMenuUIPopup 등으로 분할되어야 함(AltarUI 관련 참고) - SDH, 20250201
// TODO: 위의 작업을 한다음 MenuUI는 SingletonObject로 바뀌어야 함 - SDH, 20250208
public class MenuUI : PopupSystem
{
    public static MenuUI Instance;
    public GameObject menu;

    public override void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        base.Awake();
    }

    public override bool TurnOnPopup()
    {
        bool success = Util.SetActive(menu, true);
        if (success)
        {
            PopupManager.Instance.PushPopup(this);
            GameManager.Instance.SetDefaultCursor();
            SoundManager.Instance.SFXPlay(SoundList.Instance.menuOpen);
            if (GameManager.Instance.isInGame) UIManager.Instance.TurnOnBlur(BlurType.MenuBlackBlur);
        }
        return success;
    }

    public override bool TurnOffPopup()
    {
        bool success = Util.SetActive(menu, false);
        SoundManager.Instance.SFXPlay(SoundList.Instance.menuClose);
        UIManager.Instance.TurnOffBlur();
        PopupManager.Instance.RemovePopup(this);
        return success;
    }

    public void OnClickContinue()
    {
        TurnOffPopup();
    }

    public void OnClickOption()
    {
        UIManager.Instance.SetActiveSettingPopup(true);
    }

    public void OnClickQuit()
    {
        TurnOffPopup();
        GameManager.Instance.QuitInGame();
    }
}