using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonObject<SoundManager>
{
    public AudioSource backGroundSound;
    public AudioMixer mixer;
    private float sfxVolume;
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

        float bgVolume = (masterVolume * backgroundVolume);
        float sfxVol = (masterVolume * sfxVolume);

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
        GameObject go = new GameObject(name + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip, 1);


        Destroy(go, clip.length);
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

    public void PlayEquipSFX()
    {
        SFXPlay("Equip", SoundList.Instance.equipClip);
    }
}
