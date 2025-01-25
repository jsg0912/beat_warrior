using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniMap : MonoBehaviour
{
    public TMP_Text RemainMonster;
    public Camera MiniMapCamera;

    public GameObject PlayerMapIconPrefab;
    GameObject PlayerMapIcon;

    GameObject[] monster;
    GameObject[] icon;
    GameObject[] monsterInMap;

    int CountMapMonster;

    int MonsterInMapCount;

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
        icon = new GameObject[monster.Length];
        monsterInMap = new GameObject[monster.Length];

        CountMapMonster = 0;

        // TODO: Main Camera를 Inspector상에서 끌어놓거나, 없을 때를 대비한 코드 필요 - 신동환, 20241204
        // MainCamera[] mainCameras = FindObjectsOfType<MainCamera>();
        // mainCameras[0].GetComponent<Camera>().cullingMask = ~(1 << LayerMask.NameToLayer(LayerConstant.MiniMap));

        TryCreatePlayerMapIcon();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMonsterInfo();

        PlayerMapIcon.transform.position = Player.Instance.transform.position + Vector3.up * 0.7f;

        CountObjectInMiniMap();

        if (CountMapMonster != MonsterInMapCount)
        {

            if (CountMapMonster > MonsterInMapCount)
            {
                for (int i = MonsterInMapCount; i < CountMapMonster; i++)
                {
                    if (i >= icon.Length) continue;
                    icon[i].GetComponent<MiniMapIcon>().GetHp(0);
                }
            }
            if (CountMapMonster < MonsterInMapCount)
            {
                for (int i = CountMapMonster; i < MonsterInMapCount; i++)
                {
                    icon[i] = MyPooler.ObjectPooler.Instance.GetFromPool(PoolTag.EnemyMiniMapIcon, monsterInMap[i].transform.position, Quaternion.identity);
                }
            }

            CountMapMonster = MonsterInMapCount;
        }
        {
            for (int i = 0; i < MonsterInMapCount; i++)
            {
                if (i >= monsterInMap.Length) break;
                if (i >= icon.Length) break;
                icon[i].GetComponent<MiniMapIcon>().GetHp(monsterInMap[i].GetComponent<Monster>().GetCurrentHP());
                icon[i].GetComponent<MiniMapIcon>().GetTarget(monsterInMap[i].transform.position);
            }
        }
    }

    private void UpdateMonsterInfo()
    {
        monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
        // MiniMap에서 Monster 실시간 위치를 파악하기 위해 반드시 위치를 찾아야하다보니, 이왕 찾는거 ChapterManager의 SetMonsterCount()를 여기서 호출해 줌.
        ChapterManager.Instance.SetMonsterCount(monster.Length);
        RemainMonster.text = " Monster : " + monster.Length.ToString();
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
        int Count = 0;
        for (int i = 0; i < monster.Length; i++)
        {
            if (CheckObjectInMiniMap(monster[i]))
            {
                monsterInMap[Count++] = monster[i];
            }
            else continue;
        }
        MonsterInMapCount = Count;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryCreatePlayerMapIcon();

        monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
        icon = new GameObject[monster.Length];
        monsterInMap = new GameObject[monster.Length];
    }

    private void TryCreatePlayerMapIcon()
    {
        if (PlayerMapIcon == null) PlayerMapIcon = Instantiate(PlayerMapIconPrefab, gameObject.transform);
    }
}
