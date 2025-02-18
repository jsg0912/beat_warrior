using System;
using System.Collections.Generic;

public static class ChapterInfo
{
    public static Dictionary<ChapterName, List<SceneName>> ChapterSceneInfo = new()
    {
        {ChapterName.Tutorial, new (){SceneName.Ch2BossStage} },
        {ChapterName.Ch1, new (){SceneName.Tutorial2} },
        {ChapterName.Ch2, new (){SceneName.Ch2, SceneName.Ch2BossStage} }
    };

    public static bool CheckValid()
    {
        foreach (ChapterName chapterName in Enum.GetValues(typeof(ChapterName)))
        {
            if (!ChapterSceneInfo.ContainsKey(chapterName))
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