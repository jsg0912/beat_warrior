using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public const SettingContentIndex DefaultSettingContentIndex = SettingContentIndex.SoundSetting;
    public static SettingUIManager Instance;
    public SettingUIPopup settingUIPopup;
    private SettingContentIndex currentContentIndex;

    public void Awake()
    {
        Instance = this;
        settingUIPopup.Initialize();
        ChangeContent(DefaultSettingContentIndex);
    }

    public void TurnOnSettingUI()
    {
        settingUIPopup.TurnOnPopup();
        ChangeContent(DefaultSettingContentIndex);
    }

    public void TrunOffSettingUI()
    {
        settingUIPopup.TurnOffPopup();
    }

    public void ChangeContent(SettingContentIndex newContentIndex)
    {
        if (currentContentIndex == newContentIndex) return;

        settingUIPopup.ChangeContent(currentContentIndex, newContentIndex);

        // It updates at the end of the function.
        currentContentIndex = newContentIndex;
    }
}
