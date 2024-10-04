using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveJSON
{
    public int soul;
    public SkillName[] mySkill;

    public Chapter chapter;
    public SkillName[] equipSkill;
    public int hp;

    public Resolution resolution;
    public string[] audioName;
    public bool[] audioMute;
    public float[] audioValue;
    public KeyCode[] keyCode;

    public SaveJSON(int soul, SkillName[] mySkill)
    {
        this.soul = soul;
        this.mySkill = mySkill;
    }
    public SaveJSON(Chapter chapter, SkillName[] equipSkill, int hp)
    {
        this.chapter = chapter;
        this.equipSkill = equipSkill;
        this.hp = hp;
    }

    public SaveJSON(Resolution resolution, Dictionary<AudioList, AudioSet> audio, KeyCode[] keys)
    {
        this.resolution = resolution;
        audioName = new string[audio.Count];
        audioMute = new bool[audio.Count];
        audioValue = new float[audio.Count];
        int index = 0;
        foreach(KeyValuePair<AudioList, AudioSet>entry in audio)
        {
            audioName[index] = entry.Key.ToString();
            audioMute[index] = entry.Value.mute;
            audioValue[index] = entry.Value.volume;
            index++;
        }

        keyCode = new KeyCode[keys.Length];
        for(int i =0; i<keys.Length; i++)
        {
            keyCode[i] = keys[i];
        }

    }
}
