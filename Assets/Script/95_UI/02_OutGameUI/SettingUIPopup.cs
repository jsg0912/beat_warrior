using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingUIPopup : PopupSystem
{
    public GameObject soundSettingContent;
    public GameObject screenSettingContent;
    public GameObject keySettingContent;

    public SettingContentChangeButton soundTabButton;
    public SettingContentChangeButton screenTabButton;
    public SettingContentChangeButton keyTabButton;

    private Dictionary<SettingContentIndex, GameObject> settingContents = new();

    public TMP_Text currentContentTitle;

    public void Initialize()
    {
        settingContents.Add(SettingContentIndex.SoundSetting, soundSettingContent);
        settingContents.Add(SettingContentIndex.ScreenSetting, screenSettingContent);
        settingContents.Add(SettingContentIndex.KeySetting, keySettingContent);

        soundTabButton.SetSettingContentIndex(SettingContentIndex.SoundSetting);
        screenTabButton.SetSettingContentIndex(SettingContentIndex.ScreenSetting);
        keyTabButton.SetSettingContentIndex(SettingContentIndex.KeySetting);
    }

    private void SetCurrentContentTitle(SettingContentIndex settingContentIndex)
    {
        // currentContentTitle's Language must be English!!
        currentContentTitle.text = ScriptPool.SettingsContentTitles[settingContentIndex][Language.en];
    }

    public void ChangeContent(SettingContentIndex originalSettingContent, SettingContentIndex newSettingContent)
    {
        Util.SetActive(settingContents[originalSettingContent], false);
        Util.SetActive(settingContents[newSettingContent], true);
        SetCurrentContentTitle(newSettingContent);
    }

    public override bool TurnOffPopup()
    {
        SaveJSON saveData = SaveLoadManager.Instance.LoadMostRecentData();
        saveData.saveSetting = SettingUIManager.Instance.settingData;
        SaveLoadManager.Instance.SaveData(saveData);
        return base.TurnOffPopup();
    }
}