public class Chapter
{
    public ChapterName name { get; private set; }
    public StageController[] stages { get; private set; }

    public Chapter(ChapterName name)
    {
        this.name = name;
        stages = new StageController[StageCount.GetStageCount(name)];
    }
}