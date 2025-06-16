using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonObject<UIManager>
{
    public GameObject inGameUIPrefab;

    private void Start()
    {
        SetButtonSounds();
    }

    public void TurnOnAltarPopup()
    {
        AltarUIManager.Instance.TurnOnAltarUI();
    }

    public void SetInGameUIActive()
    {
        bool isInGame = GameManager.Instance.isInGame;
        Util.SetActive(inGameUIPrefab, GameManager.Instance.isInGame);
        if (isInGame) SetActiveMiniMap(!SceneController.Instance.GetIsBossStage(SceneController.Instance.currentScene));
    }

    public void SetActiveSettingPopup(bool isActive)
    {
        if (isActive) SettingUIManager.Instance.TurnOnSettingUI();
        else SettingUIManager.Instance.TurnOffSettingUI();
        
    }

    public void TurnOnBlur(BlurType blurType) { BlurUIManager.Instance.TurnOnActiveBlur(blurType); }

    public void TurnOffBlur() { BlurUIManager.Instance.TurnOffActiveBlur(); }

    public void SetActiveMiniMap(bool isOn) { Util.SetActive(MiniMap.Instance.gameObject, isOn); }

    public void SetButtonSounds()
    {
        Button[] allButtons = FindObjectsOfType<Button>(true);

        foreach (Button button in allButtons)
        {
        if (button.GetComponent<UIButtonSoundTrigger>() == null)
        {
            button.gameObject.AddComponent<UIButtonSoundTrigger>();
        }
        }
    }
}