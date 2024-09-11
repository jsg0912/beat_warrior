using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;

    public int defaultCapacity = 10;
    public int maxPoolSize = 20;

    public GameObject IconPrefab;
    public GameObject IconSpawnPoint;

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        Init();
    }

    private void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        for (int i = 0; i < defaultCapacity; i++)
        {
            MiniMapIcon Icon = CreatePooledItem().GetComponent<MiniMapIcon>();
            Icon.Pool.Release(Icon.gameObject);
        }
    }

    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(IconPrefab, IconSpawnPoint.transform);
        poolGo.GetComponent<MiniMapIcon>().Pool = this.Pool;
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}