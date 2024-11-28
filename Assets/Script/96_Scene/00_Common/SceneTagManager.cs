using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTagManager : MonoBehaviour
{
    // 싱글톤 구현
    public static SceneTagManager Instance { get; private set; }

    // 씬과 허용된 PoolTag 매핑
    private Dictionary<string, List<PoolTag>> sceneTagRestrictions = new Dictionary<string, List<PoolTag>>();

    // 현재 씬에서 사용 가능한 PoolTag 목록
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
        // 각 씬에 대해 허용된 PoolTag 설정
        sceneTagRestrictions.Add("TitleScene", new List<PoolTag> {  });
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
