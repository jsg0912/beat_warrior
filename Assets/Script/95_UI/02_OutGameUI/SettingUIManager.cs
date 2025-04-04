public class SettingUIManager : SingletonObject<SettingUIManager>
{
    public const SettingContentIndex DefaultSettingContentIndex = SettingContentIndex.SoundSetting;
    public SettingUIPopup settingUIPopup;
    public FadeInEffect fadeInEffect;
    private SettingContentIndex currentContentIndex;

    protected override void Awake()
    {
        base.Awake();
        settingUIPopup.Initialize();
        ChangeContent(DefaultSettingContentIndex);
    }

    public void TurnOnSettingUI()
    {
        settingUIPopup.TurnOnPopup();
        ChangeContent(DefaultSettingContentIndex);
    }

    public void TurnOffSettingUI()
    {
        settingUIPopup.TurnOffPopup();
        fadeInEffect.ShowWithFadeIn();
    }

    public void ChangeContent(SettingContentIndex newContentIndex)
    {
        if (currentContentIndex == newContentIndex) return;

        settingUIPopup.ChangeContent(currentContentIndex, newContentIndex);

        // It updates at the end of the function.
        currentContentIndex = newContentIndex;
    }
}
