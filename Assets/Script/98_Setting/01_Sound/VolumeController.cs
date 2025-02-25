using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SoundManager.Instance.MasterVolume);
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.BackGroundVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SFXVolume);

        masterSlider.value = SettingUIManager.Instance.settingData.masterVolume;
        bgmSlider.value = SettingUIManager.Instance.settingData.backgroundVolume;
        sfxSlider.value = SettingUIManager.Instance.settingData.effectVolume;
    }
}
