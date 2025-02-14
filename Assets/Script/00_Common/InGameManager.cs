using UnityEngine;

public class InGameManager : SingletonObject<InGameManager>
{
    private GameObject SoulPrefab;
    private Vector3 SoulPositionOffset = new Vector3(0, 0, 0);

    public void Start()
    {
        SoulPrefab = Resources.Load(PrefabRouter.SoulPrefab) as GameObject;
        //PauseController.Instance.ChangeDefaultGameSpeed(0.1f);
    }

    public void CreateSoul(Vector3 position)
    {
        Instantiate(SoulPrefab, position + SoulPositionOffset, Quaternion.identity);
    }
}