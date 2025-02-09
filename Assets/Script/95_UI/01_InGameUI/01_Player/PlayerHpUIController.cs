using System.Collections.Generic;
using UnityEngine;

// PlayerHpUIController는 없으면 없는대로 되어야 하기 때문에(안그러면 버그가 생김), SingletonObject를 상속받지 않는다.
public class PlayerHpUIController : MonoBehaviour
{
    public static PlayerHpUIController Instance;
    public GameObject HPPrefab;
    private List<PlayerHPUI> HPUIList = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    public void CreateAndUpdateHPUI(int hp)
    {
        CreateHPUI(hp);
        UpdateHPUI();
    }

    public void CreateHPUI(int hp)
    {
        HPUIList.Clear();
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hp; i++)
        {
            GameObject hpui = Instantiate(HPPrefab);
            hpui.transform.SetParent(transform, false);
            HPUIList.Add(hpui.GetComponent<PlayerHPUI>());
        }
    }

    public void UpdateHPUI()
    {
        int maxHP = Player.Instance.GetFinalStat(StatKind.HP);
        int currentHP = Player.Instance.GetCurrentStat(StatKind.HP);

        if (HPUIList.Count != maxHP)
        {
            Initialize();
            return;
        }

        for (int i = 0; i < maxHP; i++)
        {
            HPUIList[i].ShowHP(i < currentHP);
        }
    }

    public void Initialize()
    {
        CreateAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }
}