using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class SaveJSON
{
    //PlayData
    public int soul = 0;
    public string[] mySkill = null;
    //InGameData
    public string chapterName = ChapterName.Tutorial.ToString();
    public string[] equipSkill = null;
    public int hp = PlayerConstant.hpMax;
    //SettingData
    public int resolutionWidth = Screen.currentResolution.width;
    public int resolutionHeight = Screen.currentResolution.height;
    public string[] audioName = Enum.GetNames(typeof(AudioList));
    public float[] audioValue = new float[3] {1f, 1f, 1f};
    public string[] keyCode;

    private KeyCode[] defaultKeys = new KeyCode[]
    { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1,
        KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };
    private string[] temp;



    public SaveJSON(int soul, SkillName[] mySkill)
    {
        this.soul = soul;
        this.mySkill = Array.ConvertAll(mySkill, skillValue => skillValue.ToString());

        DefaultInGameData();
        DefaultSettingData();
    }
    public SaveJSON(Chapter chapter, SkillName[] equipSkill, int hp)
    {
        this.chapterName = chapter.name.ToString();
        this.equipSkill = Array.ConvertAll(equipSkill, skillValue => skillValue.ToString());
        this.hp = hp;

        DefaultPlayData();
        DefaultSettingData();
    }

    public SaveJSON(Resolution resolution, Dictionary<AudioList, float> audio, KeyCode[] keys)
    {
        this.resolutionWidth = resolution.width;
        this.resolutionHeight = resolution.height;
        audioName = new string[audio.Count];
        audioValue = new float[audio.Count];
        int index = 0;
        foreach(KeyValuePair<AudioList, float>entry in audio)
        {
            audioName[index] = entry.Key.ToString();
            audioValue[index] = entry.Value;
            index++;
        }

        for(int i =0; i<keys.Length; i++)
        {
            keyCode[i] = keys[i].ToString();
        }

        DefaultPlayData();
        DefaultInGameData();
    }

    private void DefaultPlayData()
    {
        SaveJSON previousData = LoadPreviousData();

        this.soul = previousData?.soul ?? 0;
        this.mySkill = previousData?.mySkill ?? null;
    }

    private void DefaultInGameData()
    {
        SaveJSON previousData = LoadPreviousData();

        this.chapterName = previousData?.chapterName ?? ChapterName.Tutorial.ToString();
        this.equipSkill = previousData?.equipSkill ?? null;
        this.hp = previousData?.hp ?? PlayerConstant.hpMax;
    }

    private void DefaultSettingData()
    {
        SaveJSON previousData = LoadPreviousData();

        this.resolutionWidth = previousData?.resolutionWidth ?? Screen.currentResolution.width;
        this.resolutionHeight = previousData?.resolutionHeight ?? Screen.currentResolution.height;
        this.audioName = previousData?.audioName ?? Enum.GetNames(typeof(AudioList));
        this.audioValue = previousData?.audioValue ?? new float[3] { 1f, 1f, 1f };
        
        for(int i = 0; i< defaultKeys.Length; i++)
        {
            temp[i] = defaultKeys[i].ToString();
        }

        this.keyCode = previousData?.keyCode ?? temp;

    }

    private SaveJSON LoadPreviousData()
    {
        string path = Application.persistentDataPath + "/SaveJSON.save";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveJSON>(json);
        }
        else
        {
            Debug.LogWarning("No previous data found.");
            return null;
        }
    }
}
