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
        // return PoolTag.EnemyMiniMapIcon;
        return enemyIcons.ContainsKey(monsterName) ? enemyIcons[monsterName] : PoolTag.MiniMapIconIppali;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트 등록
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    // Start is called before the first frame update
    void Start()
    {
        MiniMapCamera.cullingMask = 0;
        MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer(LayerConstant.MiniMap);
        MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer(LayerConstant.Tile);

        UpdateMonsterInfo();

        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer(LayerConstant.MiniMap));

        TryCreatePlayerMapIcon();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMapIcon.transform.position = Player.Instance.transform.position + Vector3.up * 0.7f;

        CountObjectInMiniMap();
        UpdateMonsterInfo();
    }

    private void UpdateMonsterInfo()
    {
        monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
        // MiniMap에서 Monster 실시간 위치를 파악하기 위해 반드시 위치를 찾아야하다보니, 이왕 찾는거 ChapterManager의 SetMonsterCount()를 여기서 호출해 줌.
        ChapterManager.Instance.SetMonsterCount(monster.Length);
        RemainMonster.text = "Remains : " + monster.Length.ToString();

        for (int i = 0; i < monsterInMap.Count; i++)
        {
            if (!monsterForIcon.Contains(monsterInMap[i]))
            {
                monsterForIcon.Add(monsterInMap[i]);
                icon.Add(MyPooler.ObjectPooler.Instance.GetFromPool(GetEnemyIconPoolTag(monsterInMap[i].GetComponent<Monster>().monsterName), monsterInMap[i].transform.position, Quaternion.identity));
            }
        }
        for (int i = 0; i < monsterForIcon.Count; i++)
        {
            if (!monsterInMap.Contains(monsterForIcon[i]))
            {
                icon[i].GetComponent<MiniMapIcon>().GetHp(0);
                icon.RemoveAt(i);
                monsterForIcon.RemoveAt(i);
            }
        }



        for (int i = 0; i < monsterForIcon.Count; i++)
        {
            if (i > icon.Count) break;
            icon[i].GetComponent<MiniMapIcon>().GetHp(monsterForIcon[i].GetComponent<Monster>().GetCurrentHP());
            icon[i].GetComponent<MiniMapIcon>().GetTarget(monsterForIcon[i].transform.position);
        }
    }

    private bool CheckObjectInMiniMap(GameObject target)
    {
        if (target == null) return false;
        Vector3 screenPoint = MiniMapCamera.WorldToViewportPoint(target.transform.position);
        bool onMiniMap = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        return onMiniMap;
    }



    private void CountObjectInMiniMap()
    {
        if (monsterInMap == null)
        {
            monsterInMap = new List<GameObject>();
        }
        monsterInMap?.Clear();

        for (int i = 0; i < monster.Length; i++)
        {
            if (CheckObjectInMiniMap(monster[i]))
            {
                monsterInMap.Add(monster[i]);
            }
            else continue;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < monsterInMap.Count; i++)
        {
            if (i >= icon.Count) break;
            icon[i].GetComponent<MiniMapIcon>().GetHp(0);
        }
        TryCreatePlayerMapIcon();
        for (int i = 0; i < icon.Count; i++)
        {
            if (i > icon.Count) continue;
            icon[i].GetComponent<MiniMapIcon>().GetHp(0);
        }
        icon.Clear();

        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer(LayerConstant.MiniMap));

        monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
        icon = new List<GameObject>();
        monsterInMap = new List<GameObject>();
        monsterForIcon = new List<GameObject>();
    }

    private void TryCreatePlayerMapIcon()
    {
        if (PlayerMapIcon == null)
        {
            PlayerMapIcon = Instantiate(PlayerMapIconPrefab, gameObject.transform);
        }
        PlayerMapIcon?.gameObject.transform.SetParent(Player.Instance.gameObject.transform);
    }
}