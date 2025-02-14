using UnityEngine;

public class InGameManager : SingletonObject<InGameManager>
{
    private GameObject SoulPrefab;
    private Vector3 SoulPositionOffset = new Vector3(0, 0, 0);
    private float SoulDropRate = 0.2f;

    public void Start()
    {
        SoulPrefab = Resources.Load(PrefabRouter.SoulPrefab) as GameObject;
        //PauseController.Instance.ChangeDefaultGameSpeed(0.1f);
    }

    public void CreateSoul(Vector3 position)
    {
        float rate = Random.Range(0.0f, 1.0f);
        if (rate < SoulDropRate)
        {
            Instantiate(SoulPrefab, position + SoulPositionOffset, Quaternion.identity);
        }
    }
}