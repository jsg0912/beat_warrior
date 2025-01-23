using System;

public static class StageCount
{
    public const int TUTORIAL = 1;
    public const int CH1 = 2;
    public const int CH2 = 6;
    public const int CH3 = 6;
    public const int CH4 = 6;

    public static int GetStageCount(ChapterName chapterName)
    {
        switch (chapterName)
        {
            case ChapterName.Tutorial:
                return TUTORIAL;
            case ChapterName.Ch1:
                return CH1;
            case ChapterName.Ch2:
                return CH2;
            case ChapterName.Ch3:
                return CH3;
            case ChapterName.Ch4:
                return CH4;
            default:
                throw new ArgumentOutOfRangeException(nameof(chapterName), chapterName, null);
        }
    }
}