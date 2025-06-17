using System;
using System.Collections.Generic;

public static class ChapterInfo
{
    public static Dictionary<ChapterName, List<List<SceneName>>> ChapterSceneInfo = new()
    {
        {ChapterName.Tutorial, new (){ new() { SceneName.Tutorial} } },
        {ChapterName.Ch1, new (){new () {SceneName.Tutorial2}} },
        {ChapterName.Ch2, new (){ new() { SceneName.Ch2}, new() { SceneName.Ch2of1, SceneName.Ch2of2, SceneName.Ch2of3}, new (){SceneName.Ch2BossStage}} }
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