using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniMap : MonoBehaviour
{
    public TMP_Text ReamaingMonster;
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

        monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
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
        monster = GameObject.FindGameObjectsWithTag(TagConstant.Monster);
        ReamaingMonster.text = "Monster : " + monster.Length.ToString();

        PlayerMapIcon.transform.position = Player.Instance.transform.position + Vector3.up * 0.7f;

        CountObjectInMiniMap();

        if (CountMapMonster != MonsterInMapCount)
        {

            if (CountMapMonster > MonsterInMapCount)
            {
                for (int i = MonsterInMapCount; i < CountMapMonster; i++)
                {
                    if (icon[i] == null) continue;
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
                if (monsterInMap[i] == null) break;
                if (icon[i] == null) break;
                icon[i].GetComponent<MiniMapIcon>().GetHp(monsterInMap[i].GetComponent<Monster>().GetCurrentHP());
                icon[i].GetComponent<MiniMapIcon>().GetTarget(monsterInMap[i].transform.position);

            }
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
