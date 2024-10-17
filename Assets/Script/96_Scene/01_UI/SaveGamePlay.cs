using System;

[System.Serializable]
public class SaveGamePlay
{
    public string currentChapterName;
    public string[] equipSkill;
    public int hp;


    public SaveGamePlay()
    {
        this.currentChapterName = ChapterName.Tutorial.ToString();
        this.equipSkill = null;
        this.hp = PlayerConstant.hpMax;
    }
    public SaveGamePlay(Chapter chapter, SkillName[] equipSkill, int hp)
    {
        this.currentChapterName = chapter.name.ToString();
        this.equipSkill = Array.ConvertAll(equipSkill, skillValue => skillValue.ToString());
        this.hp = hp;

    }

}