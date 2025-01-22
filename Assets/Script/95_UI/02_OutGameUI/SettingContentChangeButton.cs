using TMPro;
using UnityEngine;

public class SettingContentChangeButton : MonoBehaviour
{
    private SettingContentIndex settingContentIndex;
    [SerializeField] private TMP_Text buttonText;

    public void OnClick()
    {
        SettingUIManager.Instance.ChangeContent(settingContentIndex);
    }

    public void SetSettingContentIndex(SettingContentIndex index)
    {
        settingContentIndex = index;
        SetSettingButtonText();
    }

    private void SetSettingButtonText()
    {
        buttonText.text = ScriptPool.SettingsContentTitles[settingContentIndex][GameManager.Instance.language];
    }
}