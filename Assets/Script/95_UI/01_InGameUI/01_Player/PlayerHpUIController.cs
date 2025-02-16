using System.Collections.Generic;
using UnityEngine;

public class PlayerHpUIController : SingletonObject<PlayerHpUIController>
{
    public GameObject HPPrefab;
    private List<PlayerHPUI> HPUIList = new();

    private void Start()
    {
        Initialize();
    }

    public void CreateAndUpdateHPUI()
    {
        int maxHP = Player.Instance.GetFinalStat(StatKind.HP);
        CreateHPUI(maxHP);
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
        CreateAndUpdateHPUI();
    }
}