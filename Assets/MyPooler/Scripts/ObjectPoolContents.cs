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
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Koppulso)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.MiniMapIconIbkkugi,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Ibkkugi)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.MiniMapIconIppali,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Ippali)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.EnemyMiniMapIcon,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Ippali)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.MiniMapIconGiljjugi,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Giljjugi)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }, {
                PoolTag.MiniMapIconDulduli,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Dulduli)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            },{
                PoolTag.MiniMapIconItmomi,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MiniMapIconRoute(MonsterName.Itmomi)),
                    amount = 5,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.IbkkugiThrow,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MonsterAttackPrefab[MonsterName.Ibkkugi]),
                    amount = 10,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.ItmomiThrow,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MonsterAttackPrefab[MonsterName.Itmomi]),
                    amount = 10,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.Soul,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.SoulPrefab),
                    amount = 10,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.Ghost,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.GhostPrefab),
                    amount = 10,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.Mark,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.MarkerPrefab),
                    amount = 2,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.Tentacle,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.TentaclePrefab),
                    amount = 2,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.TentacleVertical,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.TentacleVerticalPrefab),
                    amount = 2,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.TentacleHorizontal,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.TentacleHorizontalPrefab),
                    amount = 2,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.IppaliEgg,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.IppaliEggPrefab),
                    amount = 2,
                    shouldExpandPool = true,
                }
            },
            {
                PoolTag.Ippali,
                new ObjectPoolContent
                {
                    prefab = Resources.Load<GameObject>(PrefabRouter.IppaliPrefab),
                    amount = 5,
                    shouldExpandPool = true,
                }
            }
        };
    }
}

