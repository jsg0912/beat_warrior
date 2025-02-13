using UnityEngine;

public class UIManager : SingletonObject<UIManager>
{
    public GameObject inGameUIPrefab;

    public void TurnOnAltarPopup()
    {
        AltarUIManager.Instance.TurnOnAltarUI();
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