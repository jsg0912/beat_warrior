using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<ChapterName, Chapter> chapters;

    private Chapter currentChapter;
    private int currentStage;
    private bool tutorialCompleted;

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

        InitializeChapters();
    }

    private void InitializeChapters()
    {
        // 챕터 초기화 (예시로 하드코딩)
        chapters = new Dictionary<ChapterName, Chapter>
        {
            { ChapterName.Tutorial, new Chapter(ChapterName.Tutorial, StageCount.TUTORIAL) },
            { ChapterName.Ch1, new Chapter(ChapterName.Ch1, StageCount.CH1) },
            { ChapterName.Ch2, new Chapter(ChapterName.Ch1, StageCount.CH2) },
            { ChapterName.Ch3, new Chapter(ChapterName.Ch1, StageCount.CH3) },
            { ChapterName.Ch4, new Chapter(ChapterName.Ch1, StageCount.CH4) }
        };
    }

    public void StartNewGame()
    {
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
            currentStage = 1;
            Debug.Log($"Started {chapterName}");
        }
        else
        {
            Debug.LogError($"Chapter {chapterName} does not exist");
        }
    }

    public void CompleteStage()
    {
        if (currentChapter != null)
        {
            if (currentStage < currentChapter.stages.Length)
            {
                currentStage++;
                Debug.Log($"Proceeding to Stage {currentStage} in {currentChapter.name}");
            }
            else
            {
                Debug.Log($"{currentChapter.name} completed!");
                MoveToNextChapter();
            }
        }
        else
        {
            Debug.LogError("No chapter is currently active");
        }
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

    public void CompleteTutorial()
    {
        tutorialCompleted = true;
        Debug.Log("Tutorial completed!");
    }
}

