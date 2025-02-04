using System;

[System.Serializable]
public class SaveGamePlay
{
    public string currentChapterName;
    public string[] equipSkill;
    public int hp;


    public SaveGamePlay()
    {
        currentChapterName = ChapterName.Tutorial.ToString();
        equipSkill = null;
        hp = Player.Instance.Hp;
    }
    public SaveGamePlay(Chapter chapter, SkillName[] equipSkill, int hp)
    {
        currentChapterName = chapter.name.ToString();
        this.equipSkill = Array.ConvertAll(equipSkill, skillValue => skillValue.ToString());
        this.hp = hp;

    }

}