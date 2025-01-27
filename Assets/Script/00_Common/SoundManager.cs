using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource backGroundSound;
    public AudioClip backGroundClip;
    public AudioMixer mixer;
    private float sfxVolume;
    private float backgroundVolume;
    private float masterVolume;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            BackGroundPlay(backGroundClip);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BackGroundVolume(float val)
    {
        backgroundVolume = val;
        if (val == 0f)
        {
            mixer.SetFloat("BackGroundSound", -80);
            return;
        }
        mixer.SetFloat("BackGroundSound", Mathf.Log10(masterVolume * backgroundVolume) * 20);
    }
    public void SFXVolume(float val)
    {
        sfxVolume = val;
        if (val == 0f)
        {
            mixer.SetFloat("SFX", -80f);
            return;
        }
        mixer.SetFloat("SFX", Mathf.Log10(masterVolume * sfxVolume) * 20);
    }
    public void MasterVolume(float val)
    {
        masterVolume = val;
        if (val == 0f)
        {
            mixer.SetFloat("SFX", -80f);
            mixer.SetFloat("BackGroundSound", -80f);
            return;
        }
        mixer.SetFloat("SFX", Mathf.Log10(masterVolume * sfxVolume) * 20);
        mixer.SetFloat("BackGroundSound", Mathf.Log10(masterVolume * backgroundVolume) * 20);
    }

    public void SFXPlay(string name, AudioClip clip)
    {
        GameObject go = new GameObject(name + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();


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
}
