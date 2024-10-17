using System;

[System.Serializable]
public class SaveRecord
{
    public int soul;
    public string[] mySkill;
    public int highChapter;

    public SaveRecord()
    {
        this.soul = 0;
        this.mySkill = null;
        this.highChapter = (int)ChapterName.Tutorial;
    }
    public SaveRecord(int soul, SkillName[] mySkill, ChapterName chapter)
    {
        this.soul = soul;
        this.mySkill = Array.ConvertAll(mySkill, skillValue => skillValue.ToString());
        this.highChapter = (int)chapter;
    }
}

