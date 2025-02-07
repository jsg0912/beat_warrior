using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance;
    public static InGameManager Instance => TryCreateInGameManager();

    private GameObject SoulPrefab;
    private Vector3 SoulPositionOffset = new Vector3(0, 0, 0);

    public static InGameManager TryCreateInGameManager()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<InGameManager>();
            if (_instance == null)
            {
                GameObject go = new GameObject("InGameManager");
                _instance = go.AddComponent<InGameManager>();
                DontDestroyOnLoad(go);
            }
        }
        return _instance;
    }

    public void Start()
    {
        SoulPrefab = Resources.Load(PrefabRouter.SoulPrefab) as GameObject;
        // PauseController.instance.SetDefaultGameSpeed(0.3f);
    }

    public void CreateSoul(Vector3 position)
    {
        Instantiate(SoulPrefab, position + SoulPositionOffset, Quaternion.identity);
    }
}