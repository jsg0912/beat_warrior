using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: MonoBehaviour? - SDH, 20241204
public class SceneTagManager : MonoBehaviour
{
    public static List<PoolTag> poolTagDefault = new List<PoolTag> { PoolTag.EnemyMiniMapIcon, PoolTag.IbkkugiThrow };
    public static SceneTagManager Instance { get; private set; }

    private Dictionary<string, List<PoolTag>> sceneTagRestrictions = new Dictionary<string, List<PoolTag>>();

    private List<PoolTag> allowedTagsInScene = new List<PoolTag>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeSceneTagRestrictions();
        UpdateAllowedTags();
    }

    private void InitializeSceneTagRestrictions()
    {
        sceneTagRestrictions.Add(SceneName.ProtoType.ToString(), new List<PoolTag> { PoolTag.EnemyMiniMapIcon, PoolTag.IbkkugiThrow });
    }

    private void UpdateAllowedTags()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneTagRestrictions.ContainsKey(currentScene))
        {
            allowedTagsInScene = sceneTagRestrictions[currentScene];
        }
        else
        {
            // TODO: Default Setting for test, whole process should be refactored - SDH, 20241204
            allowedTagsInScene = poolTagDefault;
        }
    }

    public bool IsTagAllowed(PoolTag tag)
    {
        return allowedTagsInScene.Contains(tag);
    }
}
