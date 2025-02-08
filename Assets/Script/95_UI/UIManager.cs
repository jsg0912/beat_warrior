using UnityEngine;

public class UIManager : SingletonObject<UIManager>
{
    public AltarPopup altarPopup;
    public MenuUI menuUI;
    public GameObject inGameUIPrefab;

    public void TurnOnAltarPopup()
    {
        altarPopup.TurnOnPopup();
    }

    public void SetInGameUIActive()
    {
        Util.SetActive(inGameUIPrefab, GameManager.Instance.isInGame);
    }

    public void SetActiveSettingPopup(bool isActive)
    {
        if (isActive) SettingUIManager.Instance.TurnOnSettingUI();
        else SettingUIManager.Instance.TurnOffSettingUI();
    }
}