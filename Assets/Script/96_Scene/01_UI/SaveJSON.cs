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
    public Action[] keyAction;
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

    public SaveJSON(Resolution resolution, Dictionary<string, AudioSet> audio, Dictionary<Action, KeyCode> keys)
    {
        this.resolution = resolution;
        audioName = new string[audio.Count];
        audioMute = new bool[audio.Count];
        audioValue = new float[audio.Count];

        keyAction = new Action[keys.Count];
        keyCode = new KeyCode[keys.Count];

    }
}
