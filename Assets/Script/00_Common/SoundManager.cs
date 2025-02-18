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
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

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

    public void SFXPlay(AudioClip clip, string name = "")
    {
        if (clip == null)
        {
            // Debug.LogError("SoundManager: " + name + " is null");
            return;
        }
        soundEffect.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        soundEffect.PlayOneShot(clip, sfxVolume / 2);
    }

    public void PlayPlayerSFX(string playerAction)
    {
        switch (playerAction)
        {
            case PlayerConstant.attackAnimTrigger:
                SFXPlay(SoundList.Instance.playerHolyBlade);
                break;
            case PlayerConstant.jumpAnimTrigger:
                SFXPlay(SoundList.Instance.playerJump);
                break;
            case PlayerConstant.dieAnimTrigger:
                SFXPlay(SoundList.Instance.playerDead);
                break;
            case PlayerConstant.QSkill1AnimTrigger:
            case PlayerConstant.QSkill2AnimTrigger:
                SFXPlay(SoundList.Instance.playerQSkill);
                break;
            case PlayerConstant.ESkillAnimTrigger:
                SFXPlay(SoundList.Instance.playerESkill);
                break;
            case PlayerConstant.dashAnimTrigger:
                SFXPlay(SoundList.Instance.playerDash);
                break;
        }
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
        SFXPlay(SoundList.Instance.buttonClick);
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

    public void PlayChapterBGM()
    {
        switch (ChapterManager.Instance.currentChapterName)
        {
            case ChapterName.Tutorial:
                BackGroundPlay(SoundList.Instance.chapter1BGM);
                break;
            case ChapterName.Ch1:
                BackGroundPlay(SoundList.Instance.chapter1BGM);
                break;
            case ChapterName.Ch2:
                BackGroundPlay(SoundList.Instance.chapter2BGM);
                break;
            case ChapterName.Ch3:
                BackGroundPlay(SoundList.Instance.chapter3BGM);
                break;
            case ChapterName.Ch4:
                BackGroundPlay(SoundList.Instance.chapter4BGM);
                break;
            default:
                Debug.LogError($"Chapter {default} does not have BGM");
                break;
        }
    }

    public void PlayStageBGM()
    {
        switch (SceneController.Instance.currentScene) // TODO: StageName 개념 도입 필요 - 신동환, 20250217
        {
            case SceneName.Ch2BossStage:
                BackGroundPlay(SoundList.Instance.chapter2BossBGM);
                PlayBackGroundSFX(SoundList.Instance.bossBackGroundSoundEffect);
                break;
        }
    }
}