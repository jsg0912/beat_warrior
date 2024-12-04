using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTagManager : MonoBehaviour
{
    // �̱��� ����
    public static SceneTagManager Instance { get; private set; }

    // ���� ���� PoolTag ����
    private Dictionary<string, List<PoolTag>> sceneTagRestrictions = new Dictionary<string, List<PoolTag>>();

    // ���� ������ ��� ������ PoolTag ���
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
        // �� ���� ���� ���� PoolTag ����
        // TODO: Fix SceneName Hard Coding - SDH, 20241204
        sceneTagRestrictions.Add("TitleScene", new List<PoolTag> { });
        sceneTagRestrictions.Add("ProtoType", new List<PoolTag> { PoolTag.EnemyMiniMapIcon, PoolTag.IbkkugiThrow });
        sceneTagRestrictions.Add("ShopScene", new List<PoolTag> { PoolTag.EnemyMiniMapIcon, PoolTag.IbkkugiThrow });
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
            allowedTagsInScene.Clear();
        }
    }

    public bool IsTagAllowed(PoolTag tag)
    {
        return allowedTagsInScene.Contains(tag);
    }
}
