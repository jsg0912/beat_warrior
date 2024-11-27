using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    public static PlayerHpUI Instance;

    [SerializeField] private GameObject HP;
    private GameObject HPPrefab;
    private List<Image> HPList;

    private void Awake()
    {
        Instance = this;

        HPPrefab = Resources.Load("Prefab/03_UI/PlayerHP") as GameObject;
        HPList = new();
    }

    private void Start()
    {
        HpInitialize();
    }

    public void CreateAndUpdateHPUI(int hp)
    {
        CreateHPUI(hp);
        UpdateHPUI();
    }

    public void CreateHPUI(int hp)
    {
        HPList.Clear();
        foreach (Transform child in HP.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hp; i++)
        {
            GameObject hpui = Instantiate(HPPrefab);
            hpui.transform.SetParent(HP.transform, false);
            HPList.Add(hpui.transform.GetChild(0).GetComponent<Image>());
        }
    }

    public void UpdateHPUI()
    {
        int maxHP = Player.Instance.GetFinalStat(StatKind.HP);
        int currentHP = Player.Instance.GetCurrentStat(StatKind.HP);

        if (HPList.Count != maxHP)
        {
            HpInitialize();
            return;
        }

        for (int i = 0; i < maxHP; i++)
        {
            HPList[i].gameObject.SetActive(i >= currentHP);
        }
    }

    public void HpInitialize()
    {
        CreateAndUpdateHPUI(Player.Instance.GetFinalStat(StatKind.HP));
    }
}