using UnityEngine;
using System.Collections.Generic;

namespace MyPooler
{
    public class ObjectPoolContent
    {
        public GameObject prefab;
        public int amount;
        public bool shouldExpandPool = true;
        public int extensionLimit;
    }

    public static class ObjectPoolContents
    {

        public static Dictionary<PoolTag, ObjectPoolContent> infos = new Dictionary<PoolTag, ObjectPoolContent>()
        {
            {
                PoolTag.MiniMapIconKoppulso,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconKoppulso),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.MiniMapIconIbkkugi,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconIbkkugi),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.MiniMapIconIppali,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconIppali),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.EnemyMiniMapIcon,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MapMonsterIcon),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.IbkkugiThrow,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.IbkkugiThrow),
                    amount = 10,
                    shouldExpandPool = true,
                }
            }
        };
    }
}

