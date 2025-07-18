using System;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : SingletonObject<ChapterManager>
{
    public Dictionary<ChapterName, Chapter> chapters = new();

    private Chapter currentChapter;
    private StageController CurrentStage => currentChapter.stages[currentStageIndex];
    private int currentStageIndex;
    private bool tutorialCompleted = false;
    private bool IsCurrentStageCompleted => CurrentStage.Cleared;
    public ChapterName currentChapterName => currentChapter.name;

    protected override void Awake()
    {
        base.Awake();
        InitializeChaptersForNewGame();
    }

    private void InitializeChaptersForNewGame()
    {
        chapters.Clear();
        foreach (ChapterName chapter in Enum.GetValues(typeof(ChapterName)))
        {
            if (chapter != ChapterName.End) chapters.Add(chapter, new Chapter(chapter));
        }
    }

    public int GetMonsterCount() => CurrentStage.monsterCount;

    public void SetMonsterCount(int monsterCount)
    {
        CurrentStage.SetMonsterCount(monsterCount);
        if (IsCurrentStageCompleted) PlayerUIManager.Instance.SetPlayerFace(PlayerStatus.Happy, 0);
    }

    public void SetTutorialComplete()
    {
        // TODO: 저장되어야 함.
        tutorialCompleted = true;
    }

    public void StartNewGame()
    {
        InitializeChaptersForNewGame();
        if (!tutorialCompleted)
        {
            StartChapter(ChapterName.Tutorial);
        }
        else
        {
            StartChapter(ChapterName.Ch1);
        }
    }

    public void StartChapter(ChapterName chapterName)
    {
        if (chapters.ContainsKey(chapterName))
        {
            currentChapter = chapters[chapterName];
            currentStageIndex = 0;
            LoadStageScene();
            SoundManager.Instance.PlayChapterBGM();
        }
        else
        {
            Debug.LogError($"Chapter {chapterName} does not exist");
        }
    }

    public bool MoveToNextStage()
    {
        if (IsCurrentStageCompleted)
        {
            if (++currentStageIndex == currentChapter.stages.Length)
            {
                MoveToNextChapter();
            }
            else
            {
                LoadStageScene();
            }
            return true;
        }
        return false;
    }

    public void AlarmMonsterKilled(MonsterName monsterName)
    {
        // TODO: monsterName으로 통계 쌓기 - SDH, 20250123

        // TODO: currentStage의 MonsterCount Initialize 방법을 찾은 후에 아래 코드 활성화 필요.
        //currentStage.KillMonster();
    }

    public void RestartCurrentStage()
    {
        Player.Instance.ResetTransform();
        Player.Instance.SetAnimTrigger(PlayerConstant.restartAnimTrigger);
        LoadStageScene();
        // TODO: Return soul or buying Status before stage start - SDH, 20250214
    }

    private void LoadStageScene()
    {
        StartCoroutine(SceneController.Instance.ChangeSceneWithLoading(RandomSystem.GetRandom(ChapterInfo.ChapterSceneInfo[currentChapterName][currentStageIndex])));
    }

    private void MoveToNextChapter()
    {
        ChapterName nextChapter = GetNextChapter();
        if (nextChapter != ChapterName.End)
        {
            StartChapter(nextChapter);
        }
        else
        {
            Debug.Log("All chapters completed!");
            // TODO: End시 뭐함? - SDH, 20250123
        }
    }

    private ChapterName GetNextChapter()
    {
        if (currentChapterName + 1 < ChapterName.End)
        {
            return currentChapterName + 1;
        }
        return ChapterName.End;
    }
}