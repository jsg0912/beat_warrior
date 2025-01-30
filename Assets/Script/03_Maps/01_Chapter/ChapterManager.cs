using System;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    private static ChapterManager _instance;

    public static ChapterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ChapterManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("ChapterManager");
                    _instance = go.AddComponent<ChapterManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public Dictionary<ChapterName, Chapter> chapters = new();

    private Chapter currentChapter;
    private StageController CurrentStage => currentChapter.stages[currentStageIndex];
    private int currentStageIndex;
    private bool tutorialCompleted = true; // TODO: 임시로 true임 - 신동환, 20250123
    private bool IsCurrentStageCompleted => CurrentStage.Cleared;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

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
    }

    public void SetTutorialComplete(bool tutorialCompleted)
    {
        this.tutorialCompleted = tutorialCompleted;
        if (this.tutorialCompleted) Debug.Log("Tutorial completed!");
    }

    public void StartNewGame()
    {
        InitializeChaptersForNewGame();
        if (tutorialCompleted == false)
        {
            StartChapter(ChapterName.Tutorial);
        }
        else
        {
            StartChapter(ChapterName.Ch1);
        }
        Player.TryCreatePlayer();
    }

    public void StartChapter(ChapterName chapterName)
    {
        if (chapters.ContainsKey(chapterName))
        {
            currentChapter = chapters[chapterName];
            currentStageIndex = 0;
            LoadStageScene();
        }
        else
        {
            Debug.LogError($"Chapter {chapterName} does not exist");
        }
    }

    public void MoveToNextStage()
    {
        if (IsCurrentStageCompleted)
        {
            if (++currentStageIndex == currentChapter.stages.Length)
            {
                Debug.Log($"{currentChapter.name} completed!");
                MoveToNextChapter();
            }
            else
            {
                LoadStageScene();
            }
        }
    }

    public void AlarmMonsterKilled(MonsterName monsterName)
    {
        // TODO: monsterName으로 통계 쌓기 - SDH, 20250123

        // TODO: currentStage의 MonsterCount Initialize 방법을 찾은 후에 아래 코드 활성화 필요.
        //currentStage.KillMonster();
    }

    private void LoadStageScene()
    {
        SceneController.Instance.ChangeSceneWithLoading(ChapterInfo.ChapterSceneInfo[currentChapter.name][currentStageIndex]);
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
        ChapterName currentChapterName = currentChapter.name;
        if (currentChapterName + 1 < ChapterName.End)
        {
            return currentChapterName + 1;
        }
        return ChapterName.End;
    }
}