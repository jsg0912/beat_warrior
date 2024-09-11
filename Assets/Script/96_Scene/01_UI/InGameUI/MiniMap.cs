using UnityEngine;
using TMPro;

public class MiniMap : MonoBehaviour
{
    public TMP_Text ReamaingMonster;

    public Camera MainCamera;
    public Camera MiniMapCamera;

    public GameObject PlayerMapIconFrefab;
    GameObject PlayerMapIcon;

    public GameObject MonsterMapIconFrefab;

    GameObject[] monster;
    GameObject[] icon;
    GameObject[] monsterInMap;

    int CountMapMonster;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.cullingMask = ~(1 << LayerMask.NameToLayer("MiniMap"));

        MiniMapCamera.cullingMask = 0;
        MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer("MiniMap");
        MiniMapCamera.cullingMask |= 1 << LayerMask.NameToLayer("Tile");

        monster = GameObject.FindGameObjectsWithTag("Monster");
        icon = new GameObject[monster.Length];
        monsterInMap = new GameObject[monster.Length];

        CountMapMonster = 0;

        PlayerMapIcon = Instantiate(PlayerMapIconFrefab);
    }

    // Update is called once per frame
    void Update()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster");
        ReamaingMonster.text = "Monster : " + monster.Length.ToString();

        PlayerMapIcon.transform.position = Player.Instance.transform.position;
        CountObjectInMiniMap();

        if(CountMapMonster != monsterInMap.Length)
        {
            for (int i = 0; i < monsterInMap.Length; i++)
            {
                icon[i] = ObjectPoolManager.instance.Pool.Get();
            }
            for (int j = monsterInMap.Length; j < monster.Length; j++)
            {
                icon[j].GetComponent<MiniMapIcon>().GetHp(0);
            }
            CountMapMonster = monsterInMap.Length;
        }
        {
            for (int i = 0; i < monsterInMap.Length; i++)
            {
                if (monsterInMap[i] == null) break;
                else if(icon[i] == null) break;
                icon[i].GetComponent<MiniMapIcon>().GetHp(monsterInMap[i].GetComponent<Monster>().monsterUnit.GetCurrentHP());
                icon[i].GetComponent<MiniMapIcon>().GetTarget(monsterInMap[i].transform.position);

            }
            
        }

    }

    private bool CheckObjectInMiniMap(GameObject target)
    {
        if (target == null) return false;
        Vector3 screenPoint = MiniMapCamera.WorldToViewportPoint(target.transform.position);
        bool onMiniMap = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1.5 && screenPoint.y > 0 && screenPoint.y < 1;

        return onMiniMap;
    }

    private void CountObjectInMiniMap()
    {
        int count = 0;
        for (int i = 0; i<monster.Length; i++)
        {
            if (CheckObjectInMiniMap(monster[i]))
            {
                monsterInMap[count++] = monster[i];
            }
            else continue;
        }
    }
}
