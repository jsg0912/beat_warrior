using UnityEngine;

public class InGameManager : SingletonObject<InGameManager>
{

    // public void Start()
    // {
    //     PauseController.Instance.ChangeDefaultGameSpeed(0.1f);
    // }

    public void CreateSoul(Vector3 position, float dropRate)
    {
        if (RandomSystem.RandomBool(dropRate))
        {
            MyPooler.ObjectPooler.Instance.GetFromPool(PoolTag.Soul, position, Quaternion.identity);
        }
    }
}