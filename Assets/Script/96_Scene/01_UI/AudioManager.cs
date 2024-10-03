using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class VolumeControl
{
    public Slider volumeSlider;
    public Toggle muteToggle; 
    public string exposedParameter; 

    [HideInInspector] public float volume = 1f; 
}

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;  

    public VolumeControl masterControl;  
    public VolumeControl bgmControl;      
    public VolumeControl sfxControl;     

    void Start()
    {
        masterControl.exposedParameter = "Master";
        bgmControl.exposedParameter = "BGM";
        sfxControl.exposedParameter = "SFX";

        InitializeVolumeControl(masterControl);
        InitializeVolumeControl(bgmControl);
        InitializeVolumeControl(sfxControl);

        UpdateAudioVolume(masterControl);
        UpdateAudioVolume(bgmControl);
        UpdateAudioVolume(sfxControl);
    }

    private void InitializeVolumeControl(VolumeControl control)
    {
        control.volumeSlider.onValueChanged.AddListener((value) => {
            control.volume = value;
            UpdateAudioVolume(control);
        });

        control.muteToggle.onValueChanged.AddListener((isMuted) => {
            UpdateAudioVolume(control);
        });
    }

    private void UpdateAudioVolume(VolumeControl control)
    {
        float finalVolume = control.muteToggle.isOn ? -80f : Mathf.Log10(control.volume) * 20f;  

        audioMixer.SetFloat(control.exposedParameter, finalVolume);
    }
}
