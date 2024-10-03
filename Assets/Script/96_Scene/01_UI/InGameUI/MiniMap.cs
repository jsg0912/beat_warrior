using UnityEngine;
using TMPro;

public class MiniMap : MonoBehaviour
{
    public TMP_Text ReamaingMonster;
    public Camera MiniMapCamera;

    public GameObject PlayerMapIconFrefab;
    GameObject PlayerMapIcon;

    public GameObject MonsterMapIconFrefab;

    GameObject[] monster;
    GameObject[] icon;
    GameObject[] monsterInMap;

    int CountMapMonster;

    int MonsterInMapCount;

    // Start is called before the first frame update
    void Start()
    {


        MiniMapCamera.cullingMask = 0;
        MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer("MiniMap");
        MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer("Tile");

        monster = GameObject.FindGameObjectsWithTag("Monster");
        icon = new GameObject[monster.Length];
        monsterInMap = new GameObject[monster.Length];

        CountMapMonster = 0;

        PlayerMapIcon = Instantiate(PlayerMapIconFrefab, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster");
        ReamaingMonster.text = "Monster : " + monster.Length.ToString();

        PlayerMapIcon.transform.position = Player.Instance.transform.position;

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
                    icon[i] = MyPooler.ObjectPooler.Instance.GetFromPool("EnemyMiniMapIcon", monsterInMap[i].transform.position, Quaternion.identity);
                }
            }

            CountMapMonster = MonsterInMapCount;
        }
        {
            for (int i = 0; i < MonsterInMapCount; i++)
            {
                if (monsterInMap[i] == null) break;
                if (icon[i] == null) break;
                icon[i].GetComponent<MiniMapIcon>().GetHp(monsterInMap[i].GetComponent<Monster>().monsterUnit.GetCurrentHP());
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
        for (int i = 0; i<monster.Length; i++)
        {
            if (CheckObjectInMiniMap(monster[i]))
            {
                monsterInMap[Count++] = monster[i];
            }
            else continue;
        }
        MonsterInMapCount = Count;
    }
}
