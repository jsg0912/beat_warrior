using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonObject<SoundManager>
{
    public AudioSource backGroundSound;
    public AudioSource soundEffect;
    public AudioMixer mixer;
    private float sfxVolume = 1.0f;
    private float backgroundVolume;
    private float masterVolume;

    private void ApplyVolume()
    {
        if (masterVolume == 0f)
        {
            mixer.SetFloat("SFX", -80f);
            mixer.SetFloat("BackGroundSound", -80f);
            return;
        }

        float bgVolume = masterVolume * backgroundVolume;
        float sfxVol = masterVolume * sfxVolume;

        mixer.SetFloat("BackGroundSound", bgVolume > 0 ? Mathf.Log10(bgVolume) * 20 : -80f);
        mixer.SetFloat("SFX", sfxVol > 0 ? Mathf.Log10(sfxVol) * 20 : -80f);
    }

    public void BackGroundVolume(float val)
    {
        backgroundVolume = val;
        ApplyVolume();
    }

    public void SFXVolume(float val)
    {
        sfxVolume = val;
        ApplyVolume();
    }

    public void MasterVolume(float val)
    {
        masterVolume = val;
        ApplyVolume();
    }

    public void SFXPlay(string name, AudioClip clip)
    {
        if (clip == null)
        {
            // Debug.LogError("SoundManager: " + name + " is null");
            return;
        }
        soundEffect.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        soundEffect.PlayOneShot(clip, sfxVolume / 2);
    }

    public void BackGroundPlay(AudioClip clip)
    {
        backGroundSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BackGround")[0];
        backGroundSound.clip = clip;
        backGroundSound.loop = true;
        backGroundSound.volume = 1.0f;
        backGroundSound.Play();
    }


    public void PlayTitleBGM()
    {
        BackGroundPlay(SoundList.Instance.titleBGM);
    }

    public void PlayButtonClickSFX()
    {
        SFXPlay("Equip", SoundList.Instance.buttonClick);
    }

    public void PlayBackGroundSFX(AudioClip clip)
    {
        soundEffect.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        soundEffect.clip = clip;
        soundEffect.loop = true;
        soundEffect.Play();
    }

    public void StopBackGroundSFX()
    {
        soundEffect.Stop();
    }

    public void PlayCh2BGSFX()
    {
        PlayBackGroundSFX(SoundList.Instance.bossBackGroundSoundEffect);
    }
}