using System;
using System.Collections.Generic;

public static class ChapterInfo
{
    public static Dictionary<ChapterName, List<SceneName>> ChapterSceneInfo = new()
    {
        {ChapterName.Ch1, new (){SceneName.Tutorial2, SceneName.Tutorial1 } }
    };

    public static bool CheckValid()
    {
        foreach (ChapterName chapterName in Enum.GetValues(typeof(ChapterName)))
        {
            if (ChapterSceneInfo.ContainsKey(chapterName) == false)
            {
                DebugConsole.Error($"ChapterInfo has no information for {chapterName}.");
                return false;
            }
            else
            {
                if (ChapterSceneInfo[chapterName].Count != StageCount.GetStageCount(chapterName))
                {
                    DebugConsole.Error($"ChapterInfo has not enough information for Chapter {chapterName}.");
                    return false;
                }
            }
        }
        return true;
    }
}