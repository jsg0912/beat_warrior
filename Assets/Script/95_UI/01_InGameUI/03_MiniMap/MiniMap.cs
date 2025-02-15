using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

[Serializable]
public class MiniMap : SingletonObject<MiniMap>
{
    public TMP_Text RemainMonster;
    public Camera MiniMapCamera;

    public GameObject PlayerMapIconPrefab;
    GameObject PlayerMapIcon;

    GameObject[] monster;
    private List<GameObject> icon = new List<GameObject>();
    private List<GameObject> monsterInMap = new List<GameObject>();
    private List<GameObject> monsterForIcon = new List<GameObject>();

    Dictionary<MonsterName, PoolTag> enemyIcons = new() {
        { MonsterName.Koppulso, PoolTag.MiniMapIconKoppulso },
        { MonsterName.Ippali, PoolTag.MiniMapIconIppali },
        { MonsterName.Ibkkugi, PoolTag.MiniMapIconIbkkugi },
        { MonsterName.Dulduli, PoolTag.MiniMapIconDulduli },
        { MonsterName.Giljjugi, PoolTag.MiniMapIconGiljjugi },
        { MonsterName.Itmomi, PoolTag.MiniMapIconItmomi },
    };

    private PoolTag GetEnemyIconPoolTag(MonsterName monsterName)
    {
        return enemyIcons.ContainsKey(monsterName) ? enemyIcons[monsterName] : PoolTag.MiniMapIconIppali;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        if (MiniMapCamera != null)
        {
            MiniMapCamera.cullingMask = 0;
            MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer(LayerConstant.MiniMap);
            MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer(LayerConstant.Tile);
        }

        UpdateMonsterInfo();

        if (Camera.main != null)
        {
            Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer(LayerConstant.MiniMap));
        }

        TryCreatePlayerMapIcon();
    }

    void Update()
    {
        if (PlayerMapIcon != null && Player.Instance != null)
        {
            PlayerMapIcon.transform.position = Player.Instance.GetMiddlePos();
        }

        CountObjectInMiniMap();
        UpdateMonsterInfo();
    }

    private void UpdateMonsterInfo()
    {
        try
        {
            monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
            if (ChapterManager.Instance != null)
            {
                ChapterManager.Instance.SetMonsterCount(monster.Length);
            }
            if (RemainMonster != null)
            {
                RemainMonster.text = "Remains : " + monster.Length.ToString();
            }

            for (int i = 0; i < monsterInMap.Count; i++)
            {
                if (!monsterForIcon.Contains(monsterInMap[i]))
                {
                    monsterForIcon.Add(monsterInMap[i]);
                    Monster monsterComponent = monsterInMap[i].GetComponent<Monster>();
                    if (monsterComponent != null)
                    {
                        icon.Add(MyPooler.ObjectPooler.Instance.GetFromPool(GetEnemyIconPoolTag(monsterComponent.monsterName), monsterInMap[i].transform.position, Quaternion.identity));
                    }
                }
            }

            for (int i = 0; i < monsterForIcon.Count; i++)
            {
                if (!monsterInMap.Contains(monsterForIcon[i]))
                {
                    Monster monsterComponent = monsterForIcon[i].GetComponent<Monster>();
                    if (monsterComponent != null && i < icon.Count)
                    {
                        MyPooler.ObjectPooler.Instance.ReturnToPool(GetEnemyIconPoolTag(monsterComponent.monsterName), icon[i].gameObject);
                        icon.RemoveAt(i);
                        monsterForIcon.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < monsterForIcon.Count; i++)
            {
                if (i >= icon.Count) break;
                
                GameObject obj = monsterForIcon[i];
                if (obj == null || !obj) continue;

                Monster monster = obj.GetComponent<Monster>();
                if (monster == null) continue;

                try
                {
                    int currentHP = monster.GetCurrentHP();
                    if (currentHP <= 0)
                    {
                        MyPooler.ObjectPooler.Instance.ReturnToPool(GetEnemyIconPoolTag(monster.monsterName), icon[i].gameObject);
                        icon.RemoveAt(i);
                        monsterForIcon.RemoveAt(i);
                        i--;
                        continue;
                    }
                    
                    MiniMapIcon iconComponent = icon[i].GetComponent<MiniMapIcon>();
                    if (iconComponent != null)
                    {
                        //iconComponent.GetHp(currentHP);
                        iconComponent.GetTarget(obj.transform.position);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error updating monster info: {e.Message}");
                    continue;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in UpdateMonsterInfo: {e.Message}");
        }
    }

    private bool CheckObjectInMiniMap(GameObject target)
    {
        if (target == null || MiniMapCamera == null) return false;
        Vector3 screenPoint = MiniMapCamera.WorldToViewportPoint(target.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private void CountObjectInMiniMap()
    {
        monsterInMap = monsterInMap ?? new List<GameObject>();
        monsterInMap.Clear();

        if (monster != null)
        {
            for (int i = 0; i < monster.Length; i++)
            {
                if (CheckObjectInMiniMap(monster[i]))
                {
                    monsterInMap.Add(monster[i]);
                }
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            for (int i = 0; i < monsterInMap.Count; i++)
            {
                if (i >= icon.Count) break;
                if (monsterInMap[i] != null && monsterInMap[i].GetComponent<Monster>() != null)
                {
                    MyPooler.ObjectPooler.Instance.ReturnToPool(
                        GetEnemyIconPoolTag(monsterInMap[i].GetComponent<Monster>().monsterName), 
                        icon[i].gameObject
                    );
                }
            }

            TryCreatePlayerMapIcon();

            for (int i = icon.Count - 1; i >= 0; i--)
            {
                if (icon[i] != null)
                {
                    MyPooler.ObjectPooler.Instance.ReturnToPool(PoolTag.MiniMapIconIppali, icon[i].gameObject);
                }
            }

            icon.Clear();
            monsterInMap.Clear();
            monsterForIcon.Clear();

            if (Camera.main != null)
            {
                Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer(LayerConstant.MiniMap));
            }

            monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in OnSceneLoaded: {e.Message}");
        }
    }

    private void TryCreatePlayerMapIcon()
    {
        if (PlayerMapIcon == null && PlayerMapIconPrefab != null)
        {
            PlayerMapIcon = Instantiate(PlayerMapIconPrefab, gameObject.transform);
        }
        if (PlayerMapIcon != null && Player.Instance != null)
        {
            PlayerMapIcon.transform.SetParent(Player.Instance.gameObject.transform);
        }
    }
}
