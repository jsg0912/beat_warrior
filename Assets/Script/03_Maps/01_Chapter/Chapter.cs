using System;

public class Chapter
{
    public ChapterName name { get; private set; }
    public StageController[] stages { get; private set; }

    public Chapter(ChapterName name, int stageCount)
    {
        this.name = name;
        stages = new StageController[stageCount];
    }
}