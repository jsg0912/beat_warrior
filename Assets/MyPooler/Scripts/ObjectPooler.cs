using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPooler
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public PoolTag tag;
            public GameObject prefab;
            public int amount;
            public bool shouldExpandPool = true;
            public int extensionLimit;
        }

        private static ObjectPooler _instance;
        public static ObjectPooler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ObjectPooler>();
                    if (_instance == null)
                    {
                        GameObject go = Resources.Load(PrefabRouter.ObjectPooler) as GameObject;
                        _instance = go.GetComponent<ObjectPooler>();
                    }
                }
                return _instance;
            }
        }

        void Awake()
        {
            _instance = this;
            if (!shouldDestroyOnLoad)
                DontDestroyOnLoad(this);

            CreatePools();
        }

        public bool isDebug = false;
        public bool shouldDestroyOnLoad = false;
        private int extensionSize;
        public List<Pool> pools;
        public Dictionary<string, Transform> parents;
        public Dictionary<string, Queue<GameObject>> poolDictionary;
        public Dictionary<string, List<GameObject>> activeObjects;
        public UnityAction onResetPools;

        /// <summary>
        /// Get an object from the pool if available
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                if (isDebug)
                    Debug.Log("Pool tag not found!");
                return null;
            }

            GameObject o = null;
            if (poolDictionary[tag].Count > 0)
            {
                o = poolDictionary[tag].Dequeue();
            }
            else
            {
                Pool currentPool = pools.Find(pool => pool.tag.ToString() == tag);
                float extensionLimit = currentPool.extensionLimit;

                if (currentPool.shouldExpandPool)
                {
                    if (extensionLimit > 0)
                    {
                        if (extensionSize < extensionLimit)
                        {
                            o = IncrementPool(currentPool);
                            if (isDebug)
                                Debug.Log(tag + " pool incremented!");
                            extensionSize++;
                        }
                        else
                        {
                            if (isDebug)
                                Debug.Log("You have no room left for extension on your pool: " + tag + ".");
                            return null;
                        }
                    }
                    else
                    {
                        o = IncrementPool(currentPool);
                        if (isDebug)
                            Debug.Log(tag + " pool incremented!");
                        extensionSize++;
                    }
                }
                else
                {
                    if (isDebug)
                        Debug.Log("You have no object left on your pool: " + tag + ".");
                    return null;
                }
            }
            o.SetActive(true);
            o.transform.position = position;
            o.transform.rotation = rotation;
            IPooledObject pooledObj = o.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnRequestedFromPool();
                onResetPools += pooledObj.DiscardToPool;
            }

            activeObjects[tag].Add(o);
            return o;
        }

        /// <summary>
        /// Return an object to a pool
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="o"></param>
        public void ReturnToPool(string tag, GameObject o)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                if (isDebug)
                    Debug.Log("Pool tag not found!");
                return;
            }
            activeObjects[tag].Remove(o);
            poolDictionary[tag].Enqueue(o);
            o.SetActive(false);
            if (onResetPools != null) onResetPools -= o.GetComponent<IPooledObject>().DiscardToPool;
        }

        /// <summary>
        /// Reset a entire pool 
        /// </summary>
        /// <param name="tag"></param>
        public void ResetPool(string tag)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                if (isDebug)
                    Debug.Log("Couldn't reset the pool '" + tag + "': Pool tag not found!");
                return;
            }

            List<GameObject> currentList = new List<GameObject>(activeObjects[tag]);
            foreach (GameObject go in currentList)
            {
                go.GetComponent<IPooledObject>().DiscardToPool();
            }
            currentList.Clear();
        }

        /// <summary>
        /// Reset all pools
        /// </summary>
        public void ResetAllPools()
        {
            onResetPools?.Invoke();
        }

        void CreatePools()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            parents = new Dictionary<string, Transform>();
            activeObjects = new Dictionary<string, List<GameObject>>();

            foreach (Pool pool in pools)
            {
                GameObject poolObject = new GameObject(pool.tag.ToString() + "_Pool");
                poolObject.transform.SetParent(this.transform);
                parents.Add(pool.tag.ToString(), poolObject.transform);
                activeObjects.Add(pool.tag.ToString(), new List<GameObject>());
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.amount; i++)
                {
                    GameObject o = Instantiate(pool.prefab);
                    o.SetActive(false);
                    objectPool.Enqueue(o);
                    o.transform.SetParent(poolObject.transform);
                }
                poolDictionary.Add(pool.tag.ToString(), objectPool);
            }
        }

        GameObject IncrementPool(Pool p)
        {
            GameObject objectToIncrement = p.prefab;
            GameObject obj = Instantiate(objectToIncrement);
            obj.transform.SetParent(parents[p.tag.ToString()]);
            activeObjects[p.tag.ToString()].Add(obj);
            return obj;
        }
    }
}
